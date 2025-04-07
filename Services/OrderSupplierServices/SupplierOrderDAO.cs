using Model;
using Services.Dtos;
using Services.FinanceServices;
using System;
using System.Linq;

namespace Services
{
    public class SupplierOrderDAO
    {
        public int RegisterSupplierOrder(SupplierOrderDTO dto)
        {
            using (var context = new italiapizzaEntities())
            using (var dbTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    string folio = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
                    decimal total = 0;

                    // Calcular total
                    foreach (var item in dto.Items)
                    {
                        total += item.Quantity * item.UnitPrice;
                    }

                    // Insertar encabezado en SupplierOrder
                    var order = new SupplierOrder
                    {
                        SupplierID = dto.SupplierID,
                        OrderedDate = dto.OrderedDate,
                        OrderFolio = folio,
                        Total = total,
                        Status = dto.Status ?? "En espera"
                    };

                    context.SupplierOrders.Add(order);
                    context.SaveChanges();

                    // Insertar detalle en SupplierOrder_Supply
                    foreach (var item in dto.Items)
                    {
                        var detail = new SupplierOrder_Supply
                        {
                            SupplierOrderID = order.SupplierOrderID,
                            SupplyID = item.SupplyID,
                            Quantity = item.Quantity,
                            Total = item.Quantity * item.UnitPrice
                        };

                        context.SupplierOrder_Supply.Add(detail);
                    }

                    context.SaveChanges();

                    // Registrar transacción financiera
                    var finance = new FinanceDAO();
                    finance.RegisterTransactionAndAdjustCash(new TransactionDTO
                    {
                        Type = "Salida",
                        Total = total,
                        Date = DateTime.Now,
                        Description = $"Orden de proveedor {folio}",
                        OrderID = order.SupplierOrderID
                    });

                    dbTransaction.Commit();
                    return 1;
                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();
                    Console.WriteLine("Error al registrar orden: " + ex.Message);
                    return 0;
                }
            }
        }
    }
}
