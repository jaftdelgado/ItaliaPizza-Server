using System.ServiceModel;
using Services.SupplyServices;
using Services.FinanceServices;

namespace Services
{
    [ServiceContract]
    public interface IMainManager : ICustomerManager, IPersonalManager, ISupplierManager, ISupplyManager, IProductManager, IFinanceManager, ISupplierOrderManager
    {
        [OperationContract]
        bool Ping();
    }
}
