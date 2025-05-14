using Services.Dtos;
using Services.OrderServices;

namespace Services.FinanceServices
{
    public class FinanceService : IFinanceManager
    {
        public bool RegisterOrderPayment(int orderId)
        {
            var dao = new FinanceDAO();
            return dao.RegisterOrderPayment(orderId);
        }
        public bool OpenCashRegister(decimal initialAmount)
        {
            var dao = new FinanceDAO();
            return dao.OpenCashRegister(initialAmount);
        }
    }
}
