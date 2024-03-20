using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipesProject.Contracts;
using RecipesProject.Data;
using RecipesProject.Data.Entities;
using RecipesProject.Models.RecipeViewModels;

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
            List<Recipe> recipes = await recipeService.AllAsync();
            return View(recipes);
        }
        [HttpGet]
        public async Task<IActionResult> AddRecipe()
        {
            var categories = await categoryService.AllCategoryAsync();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRecipe(AddRecipeViewModel model)
        {
            if (ModelState.IsValid)
            {
                await recipeService.AddRecipe(model);
                return RedirectToAction("Index", "Home");
            }
            return View("AllRecipes", model);
        }

        [HttpGet]
        public async Task<ActionResult<Recipe>> GetRecipeById(Guid id)
        {
            var recipe = await recipeService.GetRecipeByIdAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }
        [HttpGet]
        public async Task<IActionResult> Filter(FilterViewModel model)
        {
            var recipes = await recipeService.FilterAsync(model);
            return View("AllRecipes", model);
        }
    }
}
