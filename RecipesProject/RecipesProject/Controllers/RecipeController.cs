using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipesProject.Contracts;
using RecipesProject.Data;
using RecipesProject.Data.Entities;
using RecipesProject.Models.RecipeViewModels;
using System.Security.Claims;

namespace RecipesProject.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeService recipeService;
        private readonly ICategoryService categoryService;

        public RecipeController(IRecipeService recipeService, ICategoryService categoryService)
        {
            this.recipeService = recipeService;
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> AllRecipes()
        {
            try
            {
                List<RecipeViewModel> recipes = await recipeService.AllAsync();
                return View(recipes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddRecipe()
        {
            try
            {
                var categories = await categoryService.AllCategoryAsync();
                ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddRecipe(AddRecipeViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    await recipeService.AddRecipe(model, userId, User.IsInRole("Admin"));
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction ("AllRecipes");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetRecipeById(Guid id)
        {
            try
            {
                var recipe = await recipeService.GetRecipeByIdAsync(id);

                if (recipe == null)
                {
                    return NotFound();
                }

                return View(recipe);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Filter(FilterViewModel model)
        {
            try
            {
                var recipes = await recipeService.FilterAsync(model);
                return View("AllRecipes", recipes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> RemoveFilters(FilterViewModel model)
        {
            try
            {
                model.ServingsFilter = 0;
                model.IngredientFilter = "";
                model.TimeFilter = "";
                return RedirectToAction("AllRecipes");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserRecipes()
        {
            try
            {
                string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var recipes = await recipeService.GetUserRecipes(userId);
                return View("UserRecipes", recipes);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
