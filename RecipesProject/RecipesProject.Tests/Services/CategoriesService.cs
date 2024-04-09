using RecipesProject.Data;

namespace RecipesProject.Tests.Services
{
    internal class CategoriesService
    {
        private ApplicationDbContext data;

        public CategoriesService(ApplicationDbContext data)
        {
            this.data = data;
        }
    }
}