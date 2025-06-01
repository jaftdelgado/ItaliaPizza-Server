using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace Services
{
    public interface ISupplierDAO
    {
        List<Supplier> GetAllSuppliers();
        List<Supplier> GetSuppliersByCategory(int categoryId);
        int AddSupplier(Supplier supplier);
        bool UpdateSupplier(Supplier supplier);
        bool DeleteSupplier(int supplierId);
        bool ReactivateSupplier(int supplierId);
        bool CanDeleteSupplier(int supplierId);
    }
    public class SupplierDAO : ISupplierDAO
    {
        public List<Supplier> GetAllSuppliers()
        {
            using (var context = new italiapizzaEntities())
            {
                return context.Suppliers.ToList();
            }
        }
        public List<Supplier> GetSuppliersByCategory(int categoryId)
        {
            using (var context = new italiapizzaEntities())
            {
                return context.Suppliers
                    .Where(s => s.CategorySupply == categoryId && s.IsActive)
                    .ToList();
            }
        }
        public int AddSupplier(Supplier supplier)
        {
            using (var context = new italiapizzaEntities())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Suppliers.Add(supplier);
                        context.SaveChanges();

                        dbContextTransaction.Commit();
                        return supplier.SupplierID;
                    }
                    catch (DbEntityValidationException ex)
                    {
                        dbContextTransaction.Rollback();
                        foreach (var validationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                Console.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                            }
                        }
                        return -1;
                    }
                }
            }
        }
        public bool UpdateSupplier(Supplier updatedSupplier)
        {
            using (var context = new italiapizzaEntities())
            {
                var existingSupplier = context.Suppliers.FirstOrDefault(s => s.SupplierID == updatedSupplier.SupplierID);
                if (existingSupplier == null) return false;

                existingSupplier.SupplierName = updatedSupplier.SupplierName;
                existingSupplier.ContactName = updatedSupplier.ContactName;
                existingSupplier.PhoneNumber = updatedSupplier.PhoneNumber;
                existingSupplier.EmailAddress = updatedSupplier.EmailAddress;
                existingSupplier.Description = updatedSupplier.Description;
                existingSupplier.CategorySupply = updatedSupplier.CategorySupply;

                context.SaveChanges();
                return true;
            }
        }
        public bool DeleteSupplier(int supplierId)
        {
            using (var context = new italiapizzaEntities())
            {
                if (!CanDeleteSupplier(supplierId))
                    return false;

                var supplier = context.Suppliers.FirstOrDefault(s => s.SupplierID == supplierId);
                if (supplier == null)
                    return false;

                supplier.IsActive = false;
                context.SaveChanges();
                return true;
            }
        }

        public bool ReactivateSupplier(int supplierID)
        {
            using (var context = new italiapizzaEntities())
            {
                var supplier = context.Suppliers.FirstOrDefault(p => p.SupplierID == supplierID);
                if (supplier != null && !supplier.IsActive)
                {
                    supplier.IsActive = true;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        public bool CanDeleteSupplier(int supplierId)
        {
            using (var context = new italiapizzaEntities())
            {
                bool hasPendingOrders = context.SupplierOrders
                    .Any(o => o.SupplierID == supplierId && o.Status == 0);

                return !hasPendingOrders;
            }
        }
    }
}
