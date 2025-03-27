using System.ServiceModel;

namespace Services
{
    [ServiceContract]
    public interface IMainManager : IPersonalManager, ISupplierManager, ISupplyManager
    {
        [OperationContract]
        bool Ping();
    }
}
