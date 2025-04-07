using Services.Dtos;
using System.Collections.Generic;
using System.ServiceModel;

namespace Services
{
    [ServiceContract]
    public interface IMainManager : IPersonalManager, ISupplierManager, ISupplyManager, IProductManager
    {
        [OperationContract]
        bool Ping();
    }
}
