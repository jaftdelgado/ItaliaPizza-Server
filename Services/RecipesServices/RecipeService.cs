using Model;
using Services.Daos;
using Services.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class RecipeService : IRecipeManager
    {
        private readonly RecipeDAO recipeDAO = new RecipeDAO();

        public List<ProductDTO> GetProductsWithRecipe()
        {
            return recipeDAO.GetProductsWithRecipe();
        }

        public int AddRecipe(RecipeDTO recipeDTO)
        {
            var recipe = new Recipe
            {
                PreparationTime = recipeDTO.PreparationTime
            };

            var result = recipeDAO.AddRecipe(recipe);

            if (result != null)
            {
                recipeDTO.RecipeID = result.RecipeID;

                if (recipeDTO.Steps != null)
                {
                    foreach (var step in recipeDTO.Steps)
                    {
                        step.RecipeID = recipeDTO.RecipeID;
                        recipeDAO.AddStep(step);
                    }
                }

                if (recipeDTO.Supplies != null)
                {
                    foreach (var supply in recipeDTO.Supplies)
                    {
                        supply.RecipeID = recipeDTO.RecipeID;
                        recipeDAO.AddSupply(supply);
                    }
                }

                return recipeDTO.RecipeID;
            }

            return -1;
        }

        public bool UpdateRecipe(RecipeDTO recipeDTO)
        {
            return recipeDAO.UpdateRecipe(recipeDTO);
        }

        public bool DeleteRecipe(int recipeId)
        {
            return recipeDAO.DeleteRecipe(recipeId);
        }
    }
}
