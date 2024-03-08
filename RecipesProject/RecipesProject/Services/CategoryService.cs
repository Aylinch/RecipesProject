using Microsoft.EntityFrameworkCore;
using RecipesProject.Contracts;
using RecipesProject.Data;
using RecipesProject.Data.Entities;

namespace RecipesProject.Services
{
    public class CategoryService:ICategoryService
    {

        private readonly ApplicationDbContext dbContext;

        public CategoryService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Category>> AllCategoryAsync()
        {
            return await dbContext.Categories.ToListAsync();
        }

        public async Task<List<Recipe>> AllFromCategoriesAsync(Guid categoryId)
        {
            return await dbContext.Recipes.Where(x => x.Category.Id == categoryId).ToListAsync();
        }
    }
}
