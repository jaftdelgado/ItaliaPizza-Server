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

        public List<Recipe> GetRecipes()
        {
            using (var context = new italiapizzaEntities())
            {
                return context.Recipes.ToList();
            }
        }
    }
}
