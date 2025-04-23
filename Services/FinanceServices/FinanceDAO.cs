﻿using Model;
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
                    Type = "Ingreso",
                    Total = total,
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
    }

}
