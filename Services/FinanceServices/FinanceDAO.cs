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
                    var cashRegister = context.CashRegisters
                        .OrderByDescending(c => c.LogDate)
                        .FirstOrDefault();

                    if (cashRegister == null)
                        throw new InvalidOperationException("No se encontró caja abierta.");

                    // Validar que haya suficiente saldo
                    if (cashRegister.FinalBalance < dto.Total)
                        throw new InvalidOperationException("Saldo insuficiente en la caja.");

                    // Registrar la transacción
                    var transaction = new Transaction
                    {
                        Type = dto.Type,
                        Total = dto.Total,
                        Date = dto.Date,
                        Description = dto.Description,
                        CashRegisterID = cashRegister.CashRegisterID
                    };
                    context.Transactions.Add(transaction);

                    // Actualizar el saldo
                    cashRegister.FinalBalance = cashRegister.FinalBalance - dto.Total;

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
