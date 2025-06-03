using Model;
using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace Services.OrderServices
{
    public class OrderDAO
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
                        // Guardar entrega
                        context.Deliveries.Add(delivery);
                        context.SaveChanges();

                        // Asignar DeliveryID a la orden
                        order.DeliveryID = delivery.DeliveryID;

                        // Guardar orden
                        context.Orders.Add(order);
                        context.SaveChanges();

                        // Guardar productos de la orden
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
    }
}
