using RecipesProject.Data.Entities;
using RecipesProject.Models.RecipeViewModels;

namespace RecipesProject.Contracts
{
    public interface IRecipeService
    {
        Task<List<RecipeViewModel>> AllAsync();
        Task AddRecipe(AddRecipeViewModel model,string userId);
        Task<Recipe> GetRecipeByIdAsync(Guid id);
        Task<List<RecipeViewModel>> FilterAsync(FilterViewModel model);
        Task<List<RecipeViewModel>> GetUserRecipes(string userId);
        Task<List<RecipeViewModel>> TodaySpacial();
    }
}
