using System.ServiceModel;

namespace Services
{
    [ServiceContract]
    public interface ISupplierManager
    {
        [OperationContract]
        int RegisterSupplier(SupplierDTO supplierDTO);
    }
}
