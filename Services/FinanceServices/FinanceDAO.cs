using Model;
using Services.Dtos;
using System;
using System.Linq;

namespace Services.FinanceServices
{
    public class FinanceDAO
    {
        public bool RegisterTransactionAndAdjustCash(TransactionDTO dto)
        {
            using (var context = new italiapizzaEntities())
            using (var tx = context.Database.BeginTransaction())
            {
                try
                {
                    // Obtener la caja más reciente (último registro)
                    var cashRegister = context.CashRegisters
                        .OrderByDescending(c => c.LogDate)
                        .FirstOrDefault();

                    if (cashRegister == null)
                    {
                        throw new InvalidOperationException("No se encontró caja abierta.");
                    }

                    // Restar del saldo
                    cashRegister.FinalBalance -= dto.Total;

                    // Registrar transacción
                    context.Transactions.Add(new Transaction
                    {
                        Type = dto.Type,
                        Total = dto.Total,
                        Date = dto.Date,
                        Description = dto.Description,
                        CashRegisterID = cashRegister.CashRegisterID
                    });

                    context.SaveChanges();
                    tx.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al registrar transacción: " + ex.Message);
                    tx.Rollback();
                    return false;
                }
            }
        }
    }
}
