using Model;
using Services.Dtos;
using Services.FinanceServices;
using System;

namespace Services
{
    public class SupplierOrderDAO
    {
        public int RegisterSupplierOrder(SupplierOrderDTO dto)
        {
            using (var context = new italiapizzaEntities())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    string folio = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
                    decimal total = 0;

                    foreach (var item in dto.Items)
                    {
                        decimal lineTotal = item.Quantity * item.UnitPrice;
                        total += lineTotal;

                        var entity = new Supplier_Order
                        {
                            SupplierID = dto.SupplierID,
                            SupplyID = item.SupplyID,
                            OrderedDate = dto.OrderedDate,
                            OrderFolio = folio,
                            Total = lineTotal,
                            Quantity = item.Quantity,
                            Status = dto.Status ?? "En espera"
                        };

                        context.Supplier_Order.Add(entity);
                    }

                    var finance = new FinanceDAO();
                    finance.RegisterTransactionAndAdjustCash(new TransactionDTO
                    {
                        Type = "Salida",
                        Total = total,
                        Date = DateTime.Now,
                        Description = $"Orden de proveedor {folio}"
                    });

                    context.SaveChanges();
                    transaction.Commit();
                    return 1;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("Error al registrar orden: " + ex.Message);
                    return 0;
                }
            }
        }


    }
}
