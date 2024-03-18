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
        public async Task<Recipe> GetRecipeByIdAsync(Guid id)
        {
            var recipe = await dbContext.Recipes
                .Include(r => r.Category)
                .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .FirstOrDefaultAsync(r => r.Id == id);
            return recipe!;
        }
        public async Task<List<RecipeViewModel>> FilterRecipesAsync(FiltersViewModel model)
        {
            var query = dbContext.Recipes
                .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .AsQueryable();
            if (model.Chicken)
            {
                query = query.Where(r => r.RecipeIngredients.Any(ri => ri.Ingredient.Name.ToLower() == "chicken"));
            }
            if (model.Pork)
            {
                query = query.Where(r => r.RecipeIngredients.Any(ri => ri.Ingredient.Name.ToLower() == "pork"));
            }
            if (model.Beef)
            {
                query = query.Where(r => r.RecipeIngredients.Any(ri => ri.Ingredient.Name.ToLower() == "beef"));
            }
            if (model.Pasta)
            {
                query = query.Where(r => r.RecipeIngredients.Any(ri => ri.Ingredient.Name.ToLower().Contains("spaghetti")));
            }
            if (!string.IsNullOrEmpty(model.ServingsFilter))
            {
                if (int.TryParse(model.ServingsFilter, out int servingsValue))
                {
                    query = query.Where(r => r.Servings == servingsValue);
                }
            }

            if (!string.IsNullOrEmpty(model.IngredientFilter))
            {
                query = query.Where(r => r.RecipeIngredients.Any(ri => ri.Ingredient.Name.ToLower().Contains(model.IngredientFilter.ToLower())));
            }

            if (!string.IsNullOrEmpty(model.TimeFilters))
            {
                switch (model.TimeFilters)
                {
                    case "short":
                        query = query.Where(r => r.TotalTime <= 30);
                        break;
                    case "medium":
                        query = query.Where(r => r.TotalTime > 30 && r.TotalTime <= 60);
                        break;
                    case "long":
                        query = query.Where(r => r.TotalTime > 60);
                        break;
                }
            }

            var filteredRecipes = await query
                .Select(r => new RecipeViewModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    Instructions = r.Instructions,
                    PrepTime = r.PrepTime,  
                    CookTime = r.CookTime,  
                    TotalTime= r.TotalTime, 
                    Image=r.Image,  
                    CategoryId=r.CategoryId,
        })
                .ToListAsync();

            return filteredRecipes;
        }
    }
}
