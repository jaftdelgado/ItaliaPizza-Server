using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace Services.SupplyServices
{
    public class SupplyDAO
    {
        public List<SupplyCategory> GetCategories()
        {
            using (var context = new italiapizzaEntities())
            {
                return context.SupplyCategories.ToList();
            }
        }

        public List<Supplier> GetSuppliersByCategory(int categoryId)
        {
            using (var context = new italiapizzaEntities())
            {
                return context.Suppliers
                    .Where(s => s.CategorySupply == categoryId)
                    .ToList();
            }
        }

        public List<Supply> GetSuppliesBySupplier(int supplierId)
        {
            using (var context = new italiapizzaEntities())
            {
                return context.Supplies
                    .Where(s => s.SupplierID == supplierId)
                    .ToList();
            }
        }

        public List<Supply> GetSuppliesAvailableByCategory(int categoryId)
        {
            using (var context = new italiapizzaEntities())
            {
                return context.Supplies
                    .Where(s => s.SupplierID == null || s.SupplierID == 0)
                    .Where(s => s.SupplyCategoryID == categoryId)
                    .ToList();
            }
        }

        public List<Supply> GetAllSupplies()
        {
            using (var context = new italiapizzaEntities())
            {
                return context.Supplies.ToList();
            }
        }

        public int AddSupply(Supply supply)
        {
            int result = 0;
            using (var context = new italiapizzaEntities())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Supplies.Add(supply);
                        context.SaveChanges();

                        dbContextTransaction.Commit();
                        result = 1;
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
                        result = 0;
                    }
                }
            }
            return result;
        }

    }
}
