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
        [OperationContract]
        int RegisterCashOut(decimal amount, string description);
        [OperationContract]
        int RegisterSupplierOrderExpense(int supplierOrderID);
    }
}
