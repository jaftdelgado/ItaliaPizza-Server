using Services.Dtos;
using System.ServiceModel;

namespace Services
{
    [ServiceContract]
    public interface IWasteManager
    {
        [OperationContract]
        bool RegisterSupplyLoss(SupplyDTO supplyDTO);
    }
}
