using Microsoft.AspNetCore.Mvc;
using RecipesProject.Contracts;
using RecipesProject.Data.Entities;

namespace RecipesProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> AllCategories()
        {
            List<Category> categories = await categoryService.AllCategoryAsync();
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> AllFromCategories(Guid categoryId)
        {
            List<Recipe> recipes = await categoryService.AllFromCategoriesAsync(categoryId);
            return View(recipes);
        }
    }
}
