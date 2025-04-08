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
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    string folio = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

                    var orderEntity = new SupplierOrder
                    {
                        SupplierID = dto.SupplierID,
                        OrderedDate = dto.OrderedDate,
                        OrderFolio = folio,
                        Total = dto.Total,
                        Status = dto.Status ?? "En espera"
                    };

                    context.SupplierOrders.Add(orderEntity);
                    context.SaveChanges();

                    foreach (var item in dto.Items)
                    {
                        var itemEntity = new SupplierOrder_Supply
                        {
                            SupplierOrderID = orderEntity.SupplierOrderID,
                            SupplyID = item.SupplyID,
                            Quantity = item.Quantity,
                            Total = item.Quantity * item.UnitPrice
                        };
                        context.SupplierOrder_Supply.Add(itemEntity);
                    }

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
