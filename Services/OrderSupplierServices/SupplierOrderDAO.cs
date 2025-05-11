using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Services
{
    public class SupplierOrderDAO
    {
        public List<SupplierOrder> GetAllSupplierOrders()
        {
            using (var context = new italiapizzaEntities())
            {
                return context.SupplierOrders
                    .Include(o => o.Supplier)
                    .Include(o => o.SupplierOrder_Supply.Select(s => s.Supply))
                    .ToList();
            }
        }

        public int AddSupplierOrder(SupplierOrder order, List<SupplierOrder_Supply> orderSupplies)
        {
            int result = 0;
            using (var context = new italiapizzaEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.SupplierOrders.Add(order);
                        context.SaveChanges();

                        foreach (var item in orderSupplies)
                        {
                            item.SupplierOrderID = order.SupplierOrderID;
                            context.SupplierOrder_Supply.Add(item);
                        }

                        context.SaveChanges();
                        transaction.Commit();
                        result = 1;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        result = 0;
                    }
                }
            }
            return result;
        }

        public bool CancelSupplierOrder(int supplierOrderID)
        {
            using (var context = new italiapizzaEntities())
            {
                var order = context.SupplierOrders.FirstOrDefault(o => o.SupplierOrderID == supplierOrderID);

                if (order == null || order.Status == 2) return false;

                order.Status = 2;
                context.SaveChanges();
                return true;
            }
        }

        public bool FolioExists(string folio)
        {
            using (var context = new italiapizzaEntities())
            {
                return context.SupplierOrders.Any(o => o.OrderFolio == folio);
            }
        }
    }

}
