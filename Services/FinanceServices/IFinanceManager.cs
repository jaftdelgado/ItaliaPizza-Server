using Services.Dtos;
using System.Collections.Generic;
using System.ServiceModel;

namespace Services.FinanceServices
{
    [ServiceContract]
    public interface IFinanceManager
    {
        [OperationContract]
        List<TransactionDTO> GetCurrentTransactions();

        [OperationContract]
        bool RegisterOrderPayment(int orderId);

        [OperationContract]
        CashRegisterDTO GetOpenCashRegisterInfo();

        [OperationContract]
        bool OpenCashRegister(decimal initialAmount);

        [OperationContract]
        int RegisterCashOut(decimal amount, string description);

        [OperationContract]
        int RegisterSupplierOrderExpense(int supplierOrderID);

        [OperationContract]
        bool HasOpenCashRegister();
    }
}
