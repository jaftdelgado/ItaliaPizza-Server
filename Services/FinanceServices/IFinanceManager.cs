﻿using Services.Dtos;
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
        int RegisterOrderPayment(int orderId, decimal efectivo);

        [OperationContract]
        CashRegisterDTO GetOpenCashRegisterInfo();

        [OperationContract]
        bool OpenCashRegister(decimal initialAmount);

        [OperationContract]
        bool CloseCashRegister(decimal cashierAmount);

        [OperationContract]
        int RegisterCashOut(decimal amount, string description);

        [OperationContract]
        int RegisterSupplierOrderExpense(int supplierOrderID);

        [OperationContract]
        bool HasOpenCashRegister();
    }
}
