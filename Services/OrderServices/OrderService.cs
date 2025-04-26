using Services.Dtos;
using System.Collections.Generic;

namespace Services.OrderServices
{
    public class OrderService : IOrderManager
    {
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
    }
}
