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
            var recipes = await dbContext.Categories.ToListAsync();
            return recipes;
        }
    }
}
