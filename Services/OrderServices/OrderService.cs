using Model;
using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.OrderServices
{
    public class OrderService : IOrderManager
    {
        private readonly OrderDAO _orderDAO = new OrderDAO();

        public List<OrderDTO> GetOrders(List<int> statusList, bool includeLocal, bool includeDelivery)
        {
            return _orderDAO.GetOrders(statusList, includeLocal, includeDelivery);
        }

        public bool ChangeOrderStatus(int orderId, int newStatus, int roleId)
        {
            return _orderDAO.ChangeOrderStatus(orderId, newStatus, roleId);
        }

        public int AddLocalOrder(OrderDTO orderDTO)
        {
            var order = new Order
            {
                CustomerID = null,
                OrderDate = DateTime.Now,
                Total = orderDTO.Total,
                IsDelivery = false,
                PersonalID = orderDTO.PersonalID,
                DeliveryID = null,
                Status = 1,
                TableNumber = orderDTO.TableNumber
            };

            var productOrders = orderDTO.Items.Select(p => new Product_Order
            {
                ProductID = p.ProductID,
                Quantity = p.Quantity
            }).ToList();

            return _orderDAO.AddLocalOrder(order, productOrders);
        }

        public int AddDeliveryOrder(OrderDTO orderDTO, DeliveryDTO deliveryDTO)
        {
            var delivery = new Delivery
            {
                AddressID = deliveryDTO.AddressID,
                DeliveryDriverID = deliveryDTO.DeliveryDriverID
            };

            var order = new Order
            {
                CustomerID = orderDTO.CustomerID,
                OrderDate = DateTime.Now,
                Total = orderDTO.Total,
                IsDelivery = true,
                PersonalID = orderDTO.PersonalID,
                Status = 1,
                TableNumber = null
            };

            var productOrders = orderDTO.Items.Select(p => new Product_Order
            {
                ProductID = p.ProductID,
                Quantity = p.Quantity
            }).ToList();

            return _orderDAO.AddDeliveryOrder(order, delivery, productOrders);
        }
        public bool UpdateOrder(OrderDTO orderDTO)
        {
            var order = new Order
            {
                OrderID = orderDTO.OrderID,
                Total = orderDTO.Total
            };

            var productOrders = orderDTO.Items.Select(p => new Product_Order
            {
                ProductID = p.ProductID,
                Quantity = p.Quantity ?? 0
            }).ToList();

            return _orderDAO.UpdateOrder(order, productOrders);
        }
    }
}
