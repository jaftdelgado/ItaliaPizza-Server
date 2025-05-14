using Services.Dtos;
using System.ServiceModel;

namespace Services.FinanceServices
{
    [ServiceContract]
    public interface IFinanceManager
    {
        [OperationContract]
        bool RegisterOrderPayment(int orderId);
        [OperationContract]
        bool OpenCashRegister(decimal initialAmount);
    }
}
