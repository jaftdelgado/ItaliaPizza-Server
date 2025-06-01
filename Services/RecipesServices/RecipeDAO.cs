using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    class RecipeDAO
    {
        public int RegisterRecipe(Recipe recipe, List<RecipeSupply> recipeSupplies)
        {
            int result = 0;
            using (var context = new italiapizzaEntities())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Recipes.Add(recipe);
                        context.SaveChanges();
                        foreach (var recipeSupply in recipeSupplies)
                        {
                            recipeSupply.RecipeID = recipe.RecipeID;
                            context.RecipeSupplies.Add(recipeSupply);
                        }
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

        public List<RecipeDTO> GetAllRecipes()
        {
            using (var context = new italiapizzaEntities())
            {
                var recipes = context.Recipes
                    .Include("Product")
                    .ToList();
                return recipes.Select(r => new RecipeDTO
                {
                    RecipeID = r.RecipeID,
                    PreparationTime = r.PreparationTime,
                    ProductID = r.ProductID,
                    ProductName = r.Product.Name,
                }).ToList();
            }
        }

        public List<Recipe> GetRecipes()
        {
            using (var context = new italiapizzaEntities())
            {
                return context.Recipes.Include("Product").ToList();
            }
        }
        public int UpdateRecipe(Recipe updatedRecipe, List<RecipeSupply> updatedRecipeSupplies)
        {
            int result = 0;
            using (var context = new italiapizzaEntities())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var existingRecipe = context.Recipes
                            .Include("RecipeSupplies")
                            .FirstOrDefault(r => r.RecipeID == updatedRecipe.RecipeID);

                        if (existingRecipe == null)
                            return 0;


                        existingRecipe.PreparationTime = updatedRecipe.PreparationTime;

                        var updatedIds = updatedRecipeSupplies.Select(rs => rs.RecipeSupplyID).ToList();


                        foreach (var existingRs in existingRecipe.RecipeSupplies.ToList())
                        {
                            if (!updatedIds.Contains(existingRs.RecipeSupplyID))
                            {
                                context.RecipeSupplies.Remove(existingRs);
                            }
                        }

                        foreach (var updatedRs in updatedRecipeSupplies)
                        {
                            if (updatedRs.RecipeSupplyID == 0)
                            {
                                updatedRs.RecipeID = existingRecipe.RecipeID;
                                context.RecipeSupplies.Add(updatedRs);
                            }
                            else
                            {
                                var existingRs = existingRecipe.RecipeSupplies
                                    .FirstOrDefault(rs => rs.RecipeSupplyID == updatedRs.RecipeSupplyID);

                                if (existingRs != null)
                                {
                                    existingRs.UseQuantity = updatedRs.UseQuantity;
                                    existingRs.SupplyID = updatedRs.SupplyID;
                                }
                                else
                                {
                                    updatedRs.RecipeID = existingRecipe.RecipeID;
                                    context.RecipeSupplies.Add(updatedRs);
                                }
                            }
                        }

                        context.SaveChanges();
                        dbContextTransaction.Commit();
                        result = 1;
                    }
                    catch (DbEntityValidationException)
                    {
                        dbContextTransaction.Rollback();

                        result = 0;
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        result = 0;
                    }
                }
            }
            return result;
        }

    }
}
