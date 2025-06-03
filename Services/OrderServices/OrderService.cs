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

        public List<OrderSummaryDTO> GetDeliveredOrders()
        {
            var dao = new OrderDAO();
            return dao.GetDeliveredOrders();
        }

        public List<OrderItemSummaryDTO> GetOrderItemsByOrderID(int orderID)
        {
            var dao = new OrderDAO();
            return dao.GetOrderItemsByOrderID(orderID);
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
                Status = 0,
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
                Status = 0,
                TableNumber = null
            };

            var productOrders = orderDTO.Items.Select(p => new Product_Order
            {
                ProductID = p.ProductID,
                Quantity = p.Quantity
            }).ToList();

            return _orderDAO.AddDeliveryOrder(order, delivery, productOrders);
        }
    }
}
