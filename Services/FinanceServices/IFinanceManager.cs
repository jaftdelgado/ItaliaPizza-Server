using Services.Dtos;
using System.ServiceModel;

namespace Services.FinanceServices
{
    [ServiceContract]
    public interface IFinanceManager
    {
        [OperationContract]
        bool RegisterTransaction(TransactionDTO transaction);
    }
}