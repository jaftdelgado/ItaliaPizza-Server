using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    class RecipeService : IRecipeManager
    {
        public int RegisterRecipe(RecipeDTO recipeDTO, List<RecipeSupplyDTO> supplies)
        {
            var recipe = new Recipe
            {
                Description = recipeDTO.Description,
                PreparationTime = recipeDTO.PreparationTime,
                ProductID = recipeDTO.ProductID,
            };
            var recipeSupplies = supplies.Select(s => new RecipeSupply
            {
                SupplyID = s.SupplyID,
                UseQuantity = s.UseQuantity,
                
            }).ToList();
            var recipeDAO = new RecipeDAO();
            return recipeDAO.RegisterRecipe(recipe, recipeSupplies);
        }

        public List<RecipeDTO> GetRecipes()
        {
            var dao = new RecipeDAO();
            var recipes = dao.GetRecipes();

            return recipes.Select(r => new RecipeDTO
            {
                RecipeID = r.RecipeID,
                Description = r.Description,
                PreparationTime = r.PreparationTime
            }).ToList();
        }
    }
}
