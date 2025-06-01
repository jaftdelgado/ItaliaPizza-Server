﻿using Model;
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

        public List<RecipeDTO> GetAllRecipes()
        {
            var recipeDAO = new RecipeDAO();
            var recipes = recipeDAO.GetAllRecipes();
            return recipes;
        }
        public List<RecipeDTO> GetRecipes()
        {
            var dao = new RecipeDAO();
            var recipes = dao.GetRecipes();

            return recipes.Select(r => new RecipeDTO
            {
                RecipeID = r.RecipeID,
                PreparationTime = r.PreparationTime,
                ProductID = r.ProductID,
                ProductName = r.Product.Name,
            }).ToList();
        }

        public int UpdateRecipe(RecipeDTO recipeDTO, List<RecipeSupplyDTO> supplies)
        {
            var recipe = new Recipe
            {
                RecipeID = recipeDTO.RecipeID,
                PreparationTime = recipeDTO.PreparationTime,
                ProductID = recipeDTO.ProductID,
            };
            var recipeSupplies = supplies.Select(s => new RecipeSupply
            {
                RecipeID = recipeDTO.RecipeID,
                RecipeSupplyID = s.RecipeSupplyID,
                SupplyID = s.SupplyID,
                UseQuantity = s.UseQuantity,

            }).ToList();
            var recipeDAO = new RecipeDAO();
            return recipeDAO.UpdateRecipe(recipe, recipeSupplies);
        }
    }
}
