using Model;
using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Services.Daos
{
    public interface IRecipeDAO
    {
        List<ProductDTO> GetProductsWithRecipe(bool includeSteps = false);
        Recipe AddRecipe(Recipe recipe);
        void AddStep(RecipeStepDTO stepDTO);
        void AddSupply(RecipeSupplyDTO supplyDTO);
        bool UpdateRecipe(RecipeDTO recipeDTO);
        bool DeleteRecipe(int recipeId);
    }
    public class RecipeDAO : IRecipeDAO
    {
        public List<ProductDTO> GetProductsWithRecipe(bool includeSteps = false)
        {
            using (var context = new italiapizzaEntities())
            {
                var query = context.Products.Include("Recipe.RecipeSupplies");

                if (includeSteps)
                    query = query.Include("Recipe.RecipeSteps");

                var products = query
                    .Where(p => p.IsActive && p.RecipeID != null)
                    .AsNoTracking()
                    .ToList();

                return products.Select(p => new ProductDTO
                {
                    ProductID = p.ProductID,
                    Name = p.Name,
                    Category = p.Category,
                    Price = p.Price,
                    IsPrepared = p.IsPrepared,
                    ProductPic = p.ProductPic,
                    Description = p.Description,
                    ProductCode = p.ProductCode,
                    IsActive = p.IsActive,
                    SupplyID = p.SupplyID,
                    RecipeID = p.RecipeID,
                    Recipe = new RecipeDTO
                    {
                        RecipeID = p.Recipe.RecipeID,
                        ProductID = p.ProductID,
                        PreparationTime = p.Recipe.PreparationTime,
                        Steps = includeSteps
                            ? p.Recipe.RecipeSteps
                                .OrderBy(s => s.StepNumber)
                                .Select(s => new RecipeStepDTO
                                {
                                    RecipeStepID = s.RecipeStepID,
                                    RecipeID = s.RecipeID,
                                    StepNumber = s.StepNumber,
                                    Instruction = s.Instruction
                                }).ToList()
                            : new List<RecipeStepDTO>(),
                        Supplies = p.Recipe.RecipeSupplies
                            .Select(rs => new RecipeSupplyDTO
                            {
                                RecipeID = rs.RecipeID,
                                SupplyID = rs.SupplyID,
                                UseQuantity = rs.UseQuantity
                            }).ToList()
                    }
                }).ToList();
            }
        }

        public Recipe AddRecipe(Recipe recipe)
        {
            using (var context = new italiapizzaEntities())
            {
                context.Recipes.Add(recipe);
                context.SaveChanges();
                return recipe;
            }
        }

        public void AddStep(RecipeStepDTO stepDTO)
        {
            using (var context = new italiapizzaEntities())
            {
                var step = new RecipeStep
                {
                    RecipeID = stepDTO.RecipeID,
                    StepNumber = stepDTO.StepNumber,
                    Instruction = stepDTO.Instruction
                };

                context.RecipeSteps.Add(step);
                context.SaveChanges();
            }
        }

        public void AddSupply(RecipeSupplyDTO supplyDTO)
        {
            using (var context = new italiapizzaEntities())
            {
                var supply = new RecipeSupply
                {
                    RecipeID = supplyDTO.RecipeID,
                    SupplyID = supplyDTO.SupplyID,
                    UseQuantity = supplyDTO.UseQuantity
                };

                context.RecipeSupplies.Add(supply);
                context.SaveChanges();
            }
        }

        public bool UpdateRecipe(RecipeDTO recipeDTO)
        {
            using (var context = new italiapizzaEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var recipe = context.Recipes.Find(recipeDTO.RecipeID);
                        if (recipe == null) return false;

                        recipe.PreparationTime = recipeDTO.PreparationTime;

                        // Eliminar pasos e insumos actuales
                        var currentSteps = context.RecipeSteps.Where(s => s.RecipeID == recipeDTO.RecipeID);
                        var currentSupplies = context.RecipeSupplies.Where(s => s.RecipeID == recipeDTO.RecipeID);
                        context.RecipeSteps.RemoveRange(currentSteps);
                        context.RecipeSupplies.RemoveRange(currentSupplies);
                        context.SaveChanges();

                        // Agregar nuevos pasos
                        if (recipeDTO.Steps != null)
                        {
                            foreach (var stepDTO in recipeDTO.Steps)
                            {
                                var step = new RecipeStep
                                {
                                    RecipeID = recipeDTO.RecipeID,
                                    StepNumber = stepDTO.StepNumber,
                                    Instruction = stepDTO.Instruction
                                };
                                context.RecipeSteps.Add(step);
                            }
                        }

                        // Agregar nuevos insumos
                        if (recipeDTO.Supplies != null)
                        {
                            foreach (var supplyDTO in recipeDTO.Supplies)
                            {
                                var supply = new RecipeSupply
                                {
                                    RecipeID = recipeDTO.RecipeID,
                                    SupplyID = supplyDTO.SupplyID,
                                    UseQuantity = supplyDTO.UseQuantity
                                };
                                context.RecipeSupplies.Add(supply);
                            }
                        }

                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public bool DeleteRecipe(int recipeId)
        {
            using (var context = new italiapizzaEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var recipe = context.Recipes.Include(r => r.RecipeSteps)
                                                    .Include(r => r.RecipeSupplies)
                                                    .FirstOrDefault(r => r.RecipeID == recipeId);
                        if (recipe == null) return false;

                        context.RecipeSteps.RemoveRange(recipe.RecipeSteps);
                        context.RecipeSupplies.RemoveRange(recipe.RecipeSupplies);
                        context.Recipes.Remove(recipe);

                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }


    }
}