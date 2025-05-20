using Services.Dtos;
using System.Collections.Generic;

namespace Services.FinanceServices
{
    public class FinanceService : IFinanceManager
    {
        public List<TransactionDTO> GetCurrentTransactions()
        {
            var dao = new FinanceDAO();
            return dao.GetCurrentTransactions();
        }

        public bool RegisterOrderPayment(int orderId)
        {
            var dao = new FinanceDAO();
            return dao.RegisterOrderPayment(orderId);
        }

        public CashRegisterDTO GetOpenCashRegisterInfo()
        {
            var dao = new FinanceDAO();
            return dao.GetOpenCashRegisterInfo();
        }

        public bool OpenCashRegister(decimal initialAmount)
        {
            var dao = new FinanceDAO();
            return dao.OpenCashRegister(initialAmount);
        }

        public int RegisterCashOut(decimal amount, string description)
        {
            var dao = new FinanceDAO();
            return dao.RegisterCashOut(amount, description);
        }

        public int RegisterSupplierOrderExpense(int supplierOrderID)
        {
            var dao = new FinanceDAO();
            return dao.RegisterSupplierOrderExpense(supplierOrderID);
        }

        public bool HasOpenCashRegister()
        {
            var dao = new FinanceDAO();
            return dao.HasOpenCashRegister();
        }
    }
}