using Model;
using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.FinanceServices
{
    public interface IFinanceDAO
    {
        List<TransactionDTO> GetCurrentTransactions();
        bool RegisterOrderPayment(int orderId);
        CashRegisterDTO GetOpenCashRegisterInfo();
        bool OpenCashRegister(decimal initialAmount);
        bool CloseCashRegister(decimal cashierAmount);
        int RegisterCashOut(decimal amount, string description);
        int RegisterSupplierOrderExpense(int supplierOrderID);
        bool HasOpenCashRegister();
    }

    public class FinanceDAO : IFinanceDAO
    {
        internal CashRegister GetOpenCashRegister()
        {
            using (var context = new italiapizzaEntities())
            {
                return context.CashRegisters.FirstOrDefault(c => c.ClosingDate == null);
            }
        }

        public bool HasOpenCashRegister()
        {
            return GetOpenCashRegister() != null;
        }

        public CashRegisterDTO GetOpenCashRegisterInfo()
        {
            var cashRegister = GetOpenCashRegister();
            if (cashRegister == null) return null;

            return new CashRegisterDTO
            {
                CashRegisterID = cashRegister.CashRegisterID,
                InitialBalance = cashRegister.InitialBalance,
                CurrentBalance = cashRegister.CurrentBalance,
                OpeningDate = cashRegister.OpeningDate,
                ClosingDate = cashRegister.ClosingDate
            };
        }

        public List<TransactionDTO> GetCurrentTransactions()
        {
            using (var context = new italiapizzaEntities())
            {
                var openCashRegister = GetOpenCashRegister();
                if (openCashRegister == null)
                    return new List<TransactionDTO>();

                return context.Transactions
                    .Where(t => t.CashRegisterID == openCashRegister.CashRegisterID)
                    .OrderByDescending(t => t.Date)
                    .Select(t => new TransactionDTO
                    {
                        TransactionID = t.TransactionID,
                        CashRegisterID = t.CashRegisterID,
                        FinancialFlow = t.FinancialFlow,
                        Amount = t.Amount,
                        Date = t.Date,
                        Concept = t.Concept,
                        Description = t.Description,
                        OrderID = t.OrderID,
                        SupplierOrderID = t.SupplierOrderID
                    })
                    .ToList();
            }
        }

        public bool RegisterOrderPayment(int orderId)
        {
            using (var context = new italiapizzaEntities())
            {
                var order = context.Orders.FirstOrDefault(o => o.OrderID == orderId);
                if (order == null || order.Status != 4) return false;

                var openCashRegister = GetOpenCashRegister();
                if (openCashRegister == null) return false;

                var total = order.Total ?? 0m;

                order.Status = 5;

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

                openCashRegister.CurrentBalance += total;

                context.SaveChanges();
                return true;
            }
        }

        public bool OpenCashRegister(decimal initialAmount)
        {
            if (GetOpenCashRegister() != null)
                return false;

            using (var context = new italiapizzaEntities())
            {
                var cashRegister = new CashRegister
                {
                    OpeningDate = DateTime.Now,
                    InitialBalance = initialAmount,
                    CurrentBalance = initialAmount
                };

                context.CashRegisters.Add(cashRegister);
                context.SaveChanges();
                return true;
            }
        }

        public bool CloseCashRegister(decimal cashierAmount)
        {
            using (var context = new italiapizzaEntities())
            {
                var openCashRegister = context.CashRegisters.FirstOrDefault(c => c.ClosingDate == null);
                if (openCashRegister == null)
                    return false;

                openCashRegister.ClosingDate = DateTime.Now;
                openCashRegister.CashierAmount = cashierAmount;

                context.SaveChanges();
                return true;
            }
        }


        public int RegisterCashOut(decimal amount, string description)
        {
            using (var context = new italiapizzaEntities())
            {
                var openCashRegister = context.CashRegisters.FirstOrDefault(c => c.ClosingDate == null);
                if (openCashRegister == null)
                    return -1;

                if (openCashRegister.CurrentBalance < amount)
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
                openCashRegister.CurrentBalance -= amount;

                context.SaveChanges();
                return 1;
            }
        }
        public int RegisterSupplierOrderExpense(int supplierOrderID)
        {
            using (var context = new italiapizzaEntities())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var order = context.SupplierOrders.FirstOrDefault(o => o.SupplierOrderID == supplierOrderID);
                  
                    if (order == null || order.Status != 0) return -1;

                    var cashRegister = context.CashRegisters.FirstOrDefault(c => c.ClosingDate == null);
                    if (cashRegister == null)
                        return -2;

                    var total = order.Total;
                    if (cashRegister.CurrentBalance < total)
                        return -3;

                    var transactionRecord = new Transaction
                    {
                        FinancialFlow = "O",
                        Amount = total,
                        Date = DateTime.Now,
                        Description = $"Pago a {order.Supplier.SupplierName} - Pedido {order.OrderFolio}",
                        CashRegisterID = cashRegister.CashRegisterID,
                        SupplierOrderID = supplierOrderID,
                        Concept = 3
                    };
                    context.Transactions.Add(transactionRecord);

                    cashRegister.CurrentBalance -= total;

                    context.SaveChanges();
                    transaction.Commit();
                    return 1;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }
    }
}
