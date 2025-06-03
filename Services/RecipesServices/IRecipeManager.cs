using Services.Dtos;
using System.Collections.Generic;
using System.ServiceModel;

namespace Services
{
    [ServiceContract]
    public interface IRecipeManager
    {
        [OperationContract]
        List<ProductDTO> GetProductsWithRecipe(bool includeSteps = false);

        [OperationContract]
        int AddRecipe(RecipeDTO recipeDTO);

        [OperationContract]
        bool UpdateRecipe(RecipeDTO recipeDTO);

        [OperationContract]
        bool DeleteRecipe(int recipeId);
    }
}
