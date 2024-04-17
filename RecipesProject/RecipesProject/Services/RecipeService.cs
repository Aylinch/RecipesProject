using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecipesProject.Contracts;
using RecipesProject.Data;
using RecipesProject.Data.Account;
using RecipesProject.Data.Entities;
using RecipesProject.Models.RecipeViewModels;
using System.Linq;

namespace RecipesProject.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly ApplicationDbContext dbContext;

        public RecipeService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<RecipeViewModel>> AllAsync()
        {
            var recipes = await dbContext.Recipes
                .Where(r => r.IsApproved == true)
                .Select(recipe => new RecipeViewModel()
                {
                    Id = recipe.Id,
                    Title = recipe.Title,
                    Description = recipe.Description,
                    Instructions = recipe.Instructions,
                    PrepTime = recipe.PrepTime,
                    CookTime = recipe.CookTime,
                    TotalTime = recipe.TotalTime,
                    Servings = recipe.Servings,
                    Image = recipe.Image,
                    CategoryId = recipe.CategoryId,
                    RecipeIngredients = recipe.RecipeIngredients,

                })
                .ToListAsync();
            return recipes;
        }


        public async Task AddRecipe(AddRecipeViewModel model, string userId, bool isAdmin)
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
                CreatorId = userId
            };

            if (isAdmin)
            {
                recipe.IsApproved = true;
            }
            else
            {
                recipe.IsApproved = false;
            }
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

        public async Task<List<RecipeViewModel>> FilterAsync(FilterViewModel model)
        {
            if (model == null)
            {
                return await AllAsync();
            }
            var servings = model.ServingsFilter;
            var ingredients = model.IngredientFilter;
            var time = model.TimeFilter;
            var recipes = await dbContext.Recipes.Include(x => x.Category).Include(x => x.RecipeIngredients).ThenInclude(x => x.Ingredient).ToListAsync();
            if (servings != 0)
            {
                recipes = recipes.Where(x => x.Servings == servings).ToList();
            }
            if (ingredients != null)
            {
                if (ingredients == "chicken")
                {
                    recipes = recipes.Where(x => x.RecipeIngredients.Any(x => x.Ingredient.Name.ToLower().Contains("chicken"))).ToList();
                }
                if (ingredients == "beef")
                {
                    recipes = recipes.Where(x => x.RecipeIngredients.Any(x => x.Ingredient.Name.ToLower().Contains("beef"))).ToList();
                }   
                if (ingredients == "pork")
                {
                    recipes = recipes.Where(x => x.RecipeIngredients.Any(x => x.Ingredient.Name.ToLower().Contains("pork"))).ToList();
                }
                if (ingredients == "spaghetti")
                {
                    recipes = recipes.Where(x => x.RecipeIngredients.Any(x => x.Ingredient.Name.ToLower().Contains("spaghetti"))).ToList();
                }

            }
            if (time != null)
            {
                switch (time)
                {
                    case "fast":
                        recipes = recipes.Where(x => x.CookTime <= 20).ToList();
                        break;
                    case "medium":
                        recipes = recipes.Where(x => x.CookTime > 20 && x.PrepTime <= 45).ToList();
                        break;
                    case "slow":
                        recipes = recipes.Where(x => x.CookTime > 45).ToList();
                        break;
                }
            }
            return recipes.Where(x => x.IsApproved).Select(recipe =>
                 new RecipeViewModel
                 {
                     Id = recipe.Id,
                     Title = recipe.Title,
                     Description = recipe.Description,
                     Instructions = recipe.Instructions,
                     PrepTime = recipe.PrepTime,
                     CookTime = recipe.CookTime,
                     TotalTime = recipe.TotalTime,
                     Servings = recipe.Servings,
                     Image = recipe.Image,
                     CategoryId = recipe.CategoryId,
                     RecipeIngredients = recipe.RecipeIngredients,
                 }).ToList();
        }

        public async Task<List<RecipeViewModel>> GetUserRecipes(string userId)
        {
            var userRecipes = await dbContext.Recipes
                   .Where(x => x.CreatorId == userId)
                   .Select(r => new RecipeViewModel
                   {
                       Id = r.Id,
                       Title = r.Title,
                       Description = r.Description,
                       Instructions = r.Instructions,
                       PrepTime = r.PrepTime,
                       CookTime = r.CookTime,
                       Image = r.Image,
                       TotalTime = r.TotalTime,
                       CategoryId = r.CategoryId,
                       RecipeIngredients = r.RecipeIngredients,
                   })
                   .ToListAsync();

            return userRecipes;
        }
        public async Task<List<RecipeViewModel>> TodaySpacial()
        {
            var recipes = await dbContext.Recipes
                .Where(r => r.IsApproved == true)
                .Select(recipe => new RecipeViewModel()
                {
                    Id = recipe.Id,
                    Title = recipe.Title,
                    Description = recipe.Description,
                    Instructions = recipe.Instructions,
                    PrepTime = recipe.PrepTime,
                    CookTime = recipe.CookTime,
                    TotalTime = recipe.TotalTime,
                    Servings = recipe.Servings,
                    Image = recipe.Image,
                    CategoryId = recipe.CategoryId,
                    RecipeIngredients = recipe.RecipeIngredients,

                })
                .Take(4)
                .ToListAsync();
            return recipes;
        }
    }
}
