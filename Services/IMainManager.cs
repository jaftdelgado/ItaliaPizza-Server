using System.ServiceModel;
using Services.SupplyServices;
using Services.FinanceServices;
using Services.OrderServices;

namespace Services
{
    [ServiceContract]
    public interface IMainManager : IPersonalManager, ISupplierManager, ISupplyManager, 
        IProductManager, IFinanceManager, ISupplierOrderManager, IOrderManager, 
        IRecipeManager, ICustomerManager, ISessionManager
    {
        [OperationContract]
        bool Ping();
    }
}
