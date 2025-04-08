using Services.Dtos;
using System.Collections.Generic;
using System.ServiceModel;
using Services.SupplyServices;
using Services.FinanceServices;

namespace Services
{
    [ServiceContract]
    public interface IMainManager : IPersonalManager, ISupplierManager, ISupplyManager, IProductManager, IFinanceManager, ISupplierOrderManager, IRecipeManager
    {
        [OperationContract]
        bool Ping();
    }
}
