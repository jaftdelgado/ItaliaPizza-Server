using Services.Dtos;
using System.Collections.Generic;
using System.ServiceModel;

namespace Services
{
    [ServiceContract]
    public interface ISupplierOrderManager
    {
        [OperationContract]
        List<SupplierOrderDTO> GetAllSupplierOrders();

        [OperationContract]
        int AddSupplierOrder(SupplierOrderDTO orderDTO);

        [OperationContract]
        bool UpdateSupplierOrder(SupplierOrderDTO orderDTO);

        [OperationContract]
        bool DeliverOrder(int supplierOrderID);

        [OperationContract]
        bool CancelSupplierOrder(int supplierOrderID);

    }
}
