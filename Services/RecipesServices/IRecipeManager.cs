using System.Collections.Generic;
using System.ServiceModel;

namespace Services
{
    [ServiceContract]
    public interface IRecipeManager
    {
        [OperationContract]
        int RegisterRecipe(RecipeDTO recipeDTO, List<RecipeSupplyDTO> supplies);

        [OperationContract]
        List<RecipeDTO> GetAllRecipes();
        [OperationContract]
        List<RecipeDTO> GetRecipes();
        [OperationContract]
        int UpdateRecipe(RecipeDTO recipeDTO, List<RecipeSupplyDTO> supplies);
    }
}
