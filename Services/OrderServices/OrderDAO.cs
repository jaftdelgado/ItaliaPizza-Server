using Model;
using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace Services.OrderServices
{
    public interface IOrderDAO
    {
        List<OrderDTO> GetOrders(List<int> statusList, bool includeLocal, bool includeDelivery);
        int AddLocalOrder(Order order, List<Product_Order> productOrders);
        int AddDeliveryOrder(Order order, Delivery delivery, List<Product_Order> productOrders);
        bool ChangeOrderStatus(int orderId, int newStatus, int roleId);
        bool UpdateOrder(Order updatedOrder, List<Product_Order> updatedProducts);
    }
    public class OrderDAO : IOrderDAO
    {
        public List<OrderDTO> GetOrders(List<int> statusList, bool includeLocal, bool includeDelivery)
        {
            using (var context = new italiapizzaEntities())
            {
                var query = context.Orders.AsQueryable();

                if (statusList != null && statusList.Any())
                    query = query.Where(o => statusList.Contains(o.Status));

                if (includeLocal && !includeDelivery)
                    query = query.Where(o => o.IsDelivery == false);

                else if (!includeLocal && includeDelivery)
                    query = query.Where(o => o.IsDelivery == true);

                var orderDTOs = query.Select(order => new OrderDTO
                {
                    OrderID = order.OrderID,
                    CustomerID = order.CustomerID,
                    OrderDate = order.OrderDate,
                    Total = order.Total,
                    IsDelivery = order.IsDelivery,
                    PersonalID = order.PersonalID,
                    AttendedByName = order.Personal != null
                        ? order.Personal.FirstName + " " + order.Personal.LastName
                        : null,
                    DeliveryID = order.DeliveryID,
                    Status = order.Status,
                    TableNumber = order.TableNumber,
                    Items = order.Product_Order.Select(po => new ProductOrderDTO
                    {
                        ProductID = po.ProductID,
                        Quantity = po.Quantity,
                        Name = po.Product.Name,
                        Price = po.Product.Price,
                        ProductPic = po.Product.ProductPic
                    }).ToList(),
                    DeliveryInfo = order.IsDelivery == true && order.Customer != null && order.Customer.Address != null && order.Personal != null
                        ? new DeliveryDTO
                        {
                            DeliveryID = order.DeliveryID ?? 0,
                            AddressID = order.Customer.Address.AddressID,
                            DeliveryDriverID = order.Personal.PersonalID,
                            CustomerFullName = order.Customer.LastName + ", " + order.Customer.FirstName,
                            CustomerAddress = order.Customer.Address.AddressName + ", " + order.Customer.Address.City + ", " + order.Customer.Address.ZipCode,
                            DeliveryDriverName = order.Personal.FirstName + " " + order.Personal.LastName
                        }
                        : null
                }).ToList();

                return orderDTOs;
            }
        }

        public int AddLocalOrder(Order order, List<Product_Order> productOrders)
        {
            int result = 0;
            using (var context = new italiapizzaEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Orders.Add(order);
                        context.SaveChanges();

                        foreach (var item in productOrders)
                        {
                            item.OrderID = order.OrderID;
                            context.Product_Order.Add(item);
                        }

                        context.SaveChanges();
                        transaction.Commit();
                        result = 1;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        result = 0;
                    }
                }
            }
            return result;
        }

        public int AddDeliveryOrder(Order order, Delivery delivery, List<Product_Order> productOrders)
        {
            int result = 0;
            using (var context = new italiapizzaEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Deliveries.Add(delivery);
                        context.SaveChanges();

                        order.DeliveryID = delivery.DeliveryID;

                        context.Orders.Add(order);
                        context.SaveChanges();

                        foreach (var item in productOrders)
                        {
                            item.OrderID = order.OrderID;
                            context.Product_Order.Add(item);
                        }

                        context.SaveChanges();
                        transaction.Commit();
                        result = 1;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        result = 0;
                    }
                }
            }
            return result;
        }

        private bool IsValidTransition(int currentStatus, int newStatus, int roleId, bool isDelivery)
        {
            var transitions = new Dictionary<int, Dictionary<int, List<int>>>();

            // Cook (roleId = 5)
            var cookTransitions = new Dictionary<int, List<int>>();
            cookTransitions.Add(1, new List<int> { 2, 0 }); // Preparar, Cancelar
            cookTransitions.Add(2, new List<int> { 3, 0 }); // Listo, Cancelar
            transitions.Add(5, cookTransitions);

            // Cashier (roleId = 3)
            var cashierTransitions = new Dictionary<int, List<int>>();
            cashierTransitions.Add(1, new List<int> { 0 }); // Cancelar

            if (isDelivery)
                cashierTransitions.Add(3, new List<int> { 4, 6 }); // Enviada, Pagar
            else
                cashierTransitions.Add(3, new List<int> { 6 }); // Pagar solo

            cashierTransitions.Add(4, new List<int> { 5, 7 }); // Entregada, No entregada
            cashierTransitions.Add(5, new List<int> { 6 }); // Pagar orden
            transitions.Add(3, cashierTransitions);

            // Waiter (roleId = 4)
            var waiterTransitions = new Dictionary<int, List<int>>();
            waiterTransitions.Add(1, new List<int> { 0 }); // Cancelar
            waiterTransitions.Add(3, new List<int> { 5 }); // Marcar entregado
            transitions.Add(4, waiterTransitions);

            List<int> allowedTargets;
            Dictionary<int, List<int>> stateMap;
            if (!transitions.TryGetValue(roleId, out stateMap))
                return false;

            if (!stateMap.TryGetValue(currentStatus, out allowedTargets))
                return false;

            return allowedTargets.Contains(newStatus);
        }

        public bool ChangeOrderStatus(int orderId, int newStatus, int roleId)
        {
            using (var context = new italiapizzaEntities())
            {
                var order = context.Orders.FirstOrDefault(o => o.OrderID == orderId);
                if (order == null) throw new Exception("Orden no encontrada.");

                var isDelivery = order.IsDelivery ?? false;

                if (!IsValidTransition(order.Status, newStatus, roleId, isDelivery))
                    throw new InvalidOperationException("Transición de estado inválida para el rol actual.");

                order.Status = newStatus;
                context.SaveChanges();

                return true;
            }
        }
        public bool UpdateOrder(Order updatedOrder, List<Product_Order> updatedProducts)
        {
            using (var context = new italiapizzaEntities())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var existingOrder = context.Orders.FirstOrDefault(o => o.OrderID == updatedOrder.OrderID);
                    if (existingOrder == null)
                        return false;

                    // Actualizar el total
                    existingOrder.Total = updatedOrder.Total;

                    // Eliminar productos actuales de la orden
                    var existingProducts = context.Product_Order.Where(po => po.OrderID == updatedOrder.OrderID).ToList();
                    context.Product_Order.RemoveRange(existingProducts);
                    context.SaveChanges();

                    // Agregar nuevos productos
                    foreach (var item in updatedProducts)
                    {
                        item.OrderID = updatedOrder.OrderID;
                        context.Product_Order.Add(item);
                    }

                    context.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}
