using Model;
using Services.Dtos;
using System;
using System.Collections.Generic;
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
                    .Where(o => o.IDState == 4)
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
    }
}
