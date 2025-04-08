using Services.Dtos;
using System.Collections.Generic;
using System.ServiceModel;

namespace Services.OrderServices
{
    [ServiceContract]
    public interface IOrderManager
    {
        [OperationContract]
        bool RegisterOrderPayment(OrderDTO dto);

        [OperationContract]
        List<OrderSummaryDTO> GetDeliveredOrders();

    }

}
