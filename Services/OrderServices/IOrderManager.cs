﻿using Services.Dtos;
using System.Collections.Generic;
using System.ServiceModel;

namespace Services.OrderServices
{
    [ServiceContract]
    public interface IOrderManager
    {
        [OperationContract]
        List<OrderSummaryDTO> GetDeliveredOrders();

        [OperationContract]
        List<OrderItemSummaryDTO> GetOrderItemsByOrderID(int orderID);

        [OperationContract]
        int AddLocalOrder(OrderDTO orderDTO);

        [OperationContract]
        int AddDeliveryOrder(OrderDTO orderDTO, DeliveryDTO deliveryDTO);

    }

}
