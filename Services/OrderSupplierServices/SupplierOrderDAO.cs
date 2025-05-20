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
                    .Include(o => o.SupplierOrder_Supply.Select(s => s.Supply)).ToList();
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

        public bool UpdateSupplierOrder(SupplierOrder updatedOrder, List<SupplierOrder_Supply> updatedSupplies)
        {
            using (var context = new italiapizzaEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var existingOrder = context.SupplierOrders
                            .Include(o => o.SupplierOrder_Supply)
                            .FirstOrDefault(o => o.SupplierOrderID == updatedOrder.SupplierOrderID);

                        if (existingOrder == null || existingOrder.Status != 0) return false;

                        existingOrder.Total = updatedOrder.Total;

                        var currentSupplyIds = existingOrder.SupplierOrder_Supply.Select(s => s.SupplyID).ToList();
                        var newSupplyIds = updatedSupplies.Select(s => s.SupplyID).ToList();

                        var toRemove = existingOrder.SupplierOrder_Supply
                            .Where(s => !newSupplyIds.Contains(s.SupplyID)).ToList();

                        foreach (var item in toRemove)
                            context.SupplierOrder_Supply.Remove(item);

                        foreach (var item in updatedSupplies)
                        {
                            var existingItem = existingOrder.SupplierOrder_Supply
                                .FirstOrDefault(s => s.SupplyID == item.SupplyID);

                            if (existingItem != null)
                            {
                                existingItem.Quantity = item.Quantity;
                                existingItem.Total = item.Total;
                            }
                            else
                            {
                                item.SupplierOrderID = existingOrder.SupplierOrderID;
                                context.SupplierOrder_Supply.Add(item);
                            }
                        }

                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public bool DeliverOrder(int supplierOrderID)
        {
            using (var context = new italiapizzaEntities())
            {
                var order = context.SupplierOrders
                    .Include(o => o.SupplierOrder_Supply.Select(s => s.Supply))
                    .FirstOrDefault(o => o.SupplierOrderID == supplierOrderID);

                if (order == null || order.Status != 0) return false;

                foreach (var item in order.SupplierOrder_Supply)
                {
                    var supply = item.Supply;
                    if (supply != null)
                    {
                        supply.Stock += item.Quantity;
                    }
                }

                order.Status = 1;
                order.Delivered = DateTime.Now;

                context.SaveChanges();
                return true;
            }
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
