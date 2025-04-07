using Services.Dtos;

namespace Services.FinanceServices
{
    public class FinanceService : IFinanceManager
    {
        public bool RegisterTransaction(TransactionDTO transaction)
        {
            var dao = new FinanceDAO();
            return dao.RegisterTransactionAndAdjustCash(transaction);
        }
    }
}
