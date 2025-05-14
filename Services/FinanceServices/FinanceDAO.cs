using Model;
using Services.Dtos;
using System;
using System.Linq;

namespace Services.FinanceServices
{
    public class FinanceDAO
    {
        public bool RegisterOrderPayment(int orderId)
        {
            using (var context = new italiapizzaEntities())
            {
                var order = context.Orders.FirstOrDefault(o => o.OrderID == orderId);
                if (order == null || order.IDState != 4)
                    return false;

                var total = order.Total ?? 0m;

                var openCashRegister = context.CashRegisters
                    .FirstOrDefault(c => c.ClosingDate == null);

                if (openCashRegister == null)
                    return false;

                order.IDState = 5;

                var transaction = new Transaction
                {
                    FinancialFlow = "I",
                    Amount = total,
                    Date = DateTime.Now,
                    Description = "Pago de comanda",
                    OrderID = orderId,
                    CashRegisterID = openCashRegister.CashRegisterID
                };
                context.Transactions.Add(transaction);

                openCashRegister.FinalBalance += total;

                context.SaveChanges();
                return true;
            }
        }
        public bool OpenCashRegister(decimal initialAmount)
        {
            using (var context = new italiapizzaEntities())
            {
                // Verificar si ya hay una caja abierta
                if (context.CashRegisters.Any(c => c.ClosingDate == null))
                    return false;

                var cashRegister = new CashRegister
                {
                    OpeningDate = DateTime.Now,
                    InitialBalance = initialAmount,
                    FinalBalance = initialAmount
                };

                context.CashRegisters.Add(cashRegister);
                context.SaveChanges();
                return true;
            }
        }
        public int RegisterCashOut(decimal amount, string description)
        {
            using (var context = new italiapizzaEntities())
            {
                var openCashRegister = context.CashRegisters
                    .FirstOrDefault(c => c.ClosingDate == null);

                if (openCashRegister == null)
                    return -1;

                if (openCashRegister.FinalBalance < amount)
                    return -2;

                var transaction = new Transaction
                {
                    FinancialFlow = "O",
                    Amount = amount,
                    Date = DateTime.Now,
                    Description = description,
                    CashRegisterID = openCashRegister.CashRegisterID,
                    Concept = 2,
                    OrderID = null,
                    SupplierOrderID = null
                };

                context.Transactions.Add(transaction);
                openCashRegister.FinalBalance -= amount;

                context.SaveChanges();
                return 1;
            }
        }
    }
}
