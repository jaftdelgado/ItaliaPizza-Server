using Services.Dtos;
using System.Collections.Generic;
using System.ServiceModel;
using Services.SupplyServices;
using Services.FinanceServices;
using Services.OrderServices;

namespace Services
{
    [ServiceContract]
    public interface IMainManager : IPersonalManager, ISupplierManager, ISupplyManager, IProductManager, IFinanceManager, ISupplierOrderManager, IOrderManager
    {
        [OperationContract]
        bool Ping();
    }
}
