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
        public List<OrderSummaryDTO> GetDeliveredOrders()
        {
            using (var context = new italiapizzaEntities())
            {
                return context.Orders
                    .Where(o => o.Status == 4)
                    .Select(o => new OrderSummaryDTO
                    {
                        OrderID = o.OrderID,
                        OrderDate = o.OrderDate ?? DateTime.MinValue,
                        Total = o.Total ?? 0m,
                        Products = o.Product_Order.Select(po => new OrderItemSummaryDTO
                        {
                            Product = po.Product.Name,
                            Quantity = po.Quantity ?? 0
                        }).ToList()
                    })
                    .ToList();
            }
        }
        public List<OrderItemSummaryDTO> GetOrderItemsByOrderID(int orderID)
        {
            using (var context = new italiapizzaEntities())
            {
                return context.Product_Order
                    .Where(po => po.OrderID == orderID)
                    .Select(po => new OrderItemSummaryDTO
                    {
                        Product = po.Product.Name,
                        Quantity = po.Quantity ?? 0
                    })
                    .ToList();
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
