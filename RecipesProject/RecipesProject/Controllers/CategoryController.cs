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
            try
            {
                List<Category> categories = await categoryService.AllCategoryAsync();
                return View(categories);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> AllFromCategories(Guid categoryId)
        {
            try
            {
                List<Recipe> recipes = await categoryService.AllFromCategoriesAsync(categoryId);
                return View(recipes);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
