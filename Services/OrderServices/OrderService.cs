using Services.Dtos;
using System.Collections.Generic;

namespace Services.OrderServices
{
    public class OrderService : IOrderManager
    {
        public bool RegisterOrderPayment(OrderDTO dto)
        {
            var dao = new OrderDAO();
            return dao.RegisterOrderPayment(dto);
        }
        public List<OrderSummaryDTO> GetDeliveredOrders()
        {
            var dao = new OrderDAO();
            return dao.GetDeliveredOrders();
        }

    }
}
