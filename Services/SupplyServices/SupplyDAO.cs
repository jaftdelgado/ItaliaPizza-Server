using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public List<Supply> GetSuppliesBySupplier(int supplierId)
        {
            using (var context = new italiapizzaEntities())
            {
                return context.Supplies
                    .Where(s => s.SupplierID == supplierId && s.IsActive)
                    .ToList();
            }
        }

        public List<Supply> GetSuppliesAvailableByCategory(int categoryId, int? supplierId)
        {
            using (var context = new italiapizzaEntities())
            {
                return context.Supplies
                    .Where(s => s.SupplyCategoryID == categoryId &&
                                s.IsActive &&
                                (s.SupplierID == null || s.SupplierID == 0 || s.SupplierID == supplierId))
                    .ToList();
            }
        }

        public List<Supply> GetAllSupplies()
        {
            using (var context = new italiapizzaEntities())
            {
                return context.Supplies.Include(s => s.Supplier).ToList();
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

        public bool UpdateSupply(Supply updatedSupply)
        {
            using (var context = new italiapizzaEntities())
            {
                var existingSupply = context.Supplies.FirstOrDefault(s => s.SupplyID == updatedSupply.SupplyID);
                if (existingSupply == null)
                    return false;

                existingSupply.SupplyName = updatedSupply.SupplyName;
                existingSupply.Price = updatedSupply.Price;
                existingSupply.MeasureUnit = updatedSupply.MeasureUnit;
                existingSupply.Brand = updatedSupply.Brand;
                existingSupply.SupplyPic = updatedSupply.SupplyPic;
                existingSupply.Description = updatedSupply.Description;
                existingSupply.SupplyCategoryID = updatedSupply.SupplyCategoryID;

                context.SaveChanges();
                return true;
            }
        }

        public bool DeleteSupply(int supplyID)
        {
            using (var context = new italiapizzaEntities())
            {
                var supply = context.Supplies.FirstOrDefault(p => p.SupplyID == supplyID);
                if (supply != null)
                {
                    supply.IsActive = false;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public bool ReactivateSupply(int supplyID)
        {
            using (var context = new italiapizzaEntities())
            {
                var supply = context.Supplies.FirstOrDefault(p => p.SupplyID == supplyID);
                if (supply != null && !supply.IsActive)
                {
                    supply.IsActive = true;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public bool AssignSupplierToSupply(List<int> supplyIds, int supplierId)
        {
            using (var context = new italiapizzaEntities())
            {
                var supplies = context.Supplies
                    .Where(s => supplyIds.Contains(s.SupplyID) && s.IsActive)
                    .ToList();

                if (!supplies.Any()) return false;

                foreach (var supply in supplies)
                {
                    supply.SupplierID = supplierId;
                }

                context.SaveChanges();
                return true;
            }
        }

        public bool UnassignSupplierFromSupply(List<int> supplyIds, int supplierId)
        {
            using (var context = new italiapizzaEntities())
            {
                var supplies = context.Supplies
                    .Where(s => supplyIds.Contains(s.SupplyID) &&
                                s.SupplierID == supplierId &&
                                s.IsActive)
                    .ToList();

                if (!supplies.Any()) return false;

                foreach (var supply in supplies)
                {
                    supply.SupplierID = null;
                }

                context.SaveChanges();
                return true;
            }
        }

        public List<RecipeSupply> GetSuppliesByRecipe(int recipeId)
        {
            using (var context = new italiapizzaEntities())
            {
                return context.RecipeSupplies
                    .Include(rs => rs.Supply)
                    .Where(rs => rs.RecipeID == recipeId)
                    .ToList();
            }
        }
    }
}
