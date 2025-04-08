using Services.Dtos;
using System.ServiceModel;

namespace Services
{
    [ServiceContract]
    public interface ISupplierOrderManager
    {
        [OperationContract]
        int RegisterOrder(SupplierOrderDTO order);
    }
}
