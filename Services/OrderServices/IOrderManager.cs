using Services.Dtos;
using System.Collections.Generic;
using System.ServiceModel;

namespace Services.OrderServices
{
    [ServiceContract]
    public interface IOrderManager
    {
        [OperationContract]
        List<OrderDTO> GetOrders(List<int> statusList, bool includeLocal, bool includeDelivery);


        [OperationContract]
        int AddLocalOrder(OrderDTO orderDTO);

        [OperationContract]
        int AddDeliveryOrder(OrderDTO orderDTO, DeliveryDTO deliveryDTO);

    }

}
