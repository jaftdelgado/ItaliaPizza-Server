using Model;
using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace Services.SupplyServices
{
    public class SupplyDAO
    {
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

        public List<SupplyDTO> GetAllSupplies(bool activeOnly = false)
        {
            using (var context = new italiapizzaEntities())
            {
                var query = context.Supplies.Include(s => s.Supplier).AsQueryable();

                if (activeOnly) query = query.Where(s => s.IsActive);

                var supplies = query.ToList();

                var usedInRecipes = context.RecipeSupplies.Select(rs => rs.SupplyID).Distinct().ToHashSet();
                var withSuppliers = supplies
                    .Where(s => s.SupplierID != null)
                    .Select(s => s.SupplyID)
                    .ToHashSet();

                return supplies.Select(s => new SupplyDTO
                {
                    Id = s.SupplyID,
                    Name = s.SupplyName,
                    Price = s.Price,
                    MeasureUnit = s.MeasureUnit,
                    Brand = s.Brand,
                    SupplyPic = s.SupplyPic,
                    Description = s.Description,
                    IsActive = s.IsActive,
                    Stock = s.Stock,
                    SupplyCategoryID = s.SupplyCategoryID,
                    SupplierID = s.SupplierID,
                    SupplierName = s.Supplier?.SupplierName,
                    IsDeletable = !usedInRecipes.Contains(s.SupplyID) && !withSuppliers.Contains(s.SupplyID)
                }).ToList();
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
        public bool IsSupplyDeletable(int supplyId)
        {
            using (var context = new italiapizzaEntities())
            {
                bool hasSupplier = context.Supplies
                    .Any(s => s.SupplyID == supplyId && s.SupplierID != null);

                bool usedInRecipe = context.RecipeSupplies
                    .Any(rs => rs.SupplyID == supplyId);

                return !hasSupplier && !usedInRecipe;
            }
        }
    }
}
