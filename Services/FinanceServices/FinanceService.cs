using Services.Dtos;
using System.Collections.Generic;

namespace Services.FinanceServices
{
    public class FinanceService : IFinanceManager
    {
        private readonly IFinanceDAO _dao;
        public FinanceService() : this(new FinanceDAO()) { }

        public FinanceService(IFinanceDAO dao)
        {
            _dao = dao;
        }

        public List<TransactionDTO> GetCurrentTransactions()
        {
            return _dao.GetCurrentTransactions();
        }

        public bool RegisterOrderPayment(int orderId)
        {
            return _dao.RegisterOrderPayment(orderId);
        }

        public CashRegisterDTO GetOpenCashRegisterInfo()
        {
            return _dao.GetOpenCashRegisterInfo();
        }

        public bool OpenCashRegister(decimal initialAmount)
        {
            return _dao.OpenCashRegister(initialAmount);
        }

        public bool CloseCashRegister(decimal cashierAmount)
        {
            return _dao.CloseCashRegister(cashierAmount);
        }

        public int RegisterCashOut(decimal amount, string description)
        {
            return _dao.RegisterCashOut(amount, description);
        }

        public int RegisterSupplierOrderExpense(int supplierOrderID)
        {
            return _dao.RegisterSupplierOrderExpense(supplierOrderID);
        }

        public bool HasOpenCashRegister()
        {
            return _dao.HasOpenCashRegister();
        }
    }
}
