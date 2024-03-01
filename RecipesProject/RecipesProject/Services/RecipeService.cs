using Microsoft.EntityFrameworkCore;
using RecipesProject.Contracts;
using RecipesProject.Data;
using RecipesProject.Data.Entities;
using RecipesProject.Models.RecipeViewModels;

namespace RecipesProject.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly ApplicationDbContext dbContext;

        public RecipeService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Recipe>> AllAsync()
        {
            var recipes = await dbContext.Recipes.ToListAsync();
            return recipes;
        }


        public async Task AddRecipe(AddRecipeViewModel model)
        {
            var recipe = new Recipe
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Description = model.Description,
                Instructions = model.Instructions,
                PrepTime = model.PrepTime,
                CookTime = model.CookTime,
                TotalTime = model.TotalTime,
                Servings = model.Servings,
                Image = model.Image,
                CategoryId = model.CategoryId,
            };
            await dbContext.Recipes.AddAsync(recipe);
            foreach (var ingredientViewModel in model.Ingredients)
            {
                var ingredient = new Ingredient
                {
                    Id = ingredientViewModel.Id,
                    Name = ingredientViewModel.Name,
                };
                recipe.RecipeIngredients.Add(new RecipeIngredients
                {
                    RecipeId = recipe.Id,
                    Ingredient = ingredient,
                    IngredientQuanitity = ingredientViewModel.Quantity,
                });
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
