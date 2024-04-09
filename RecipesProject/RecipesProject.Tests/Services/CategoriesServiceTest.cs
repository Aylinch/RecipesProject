using RecipesProject.Data.Entities;
using RecipesProject.Services;
using RecipesProject.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesProject.Tests.Services
{
    [TestFixture]
    public class CategoriesServiceTest
    {
        [Test]
        public async Task ListCategories_ReturnsAllCategories()
        {
            using var data = DatabaseMock.Instance;

            data.Categories.Add(new Category
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                CategoryImage = "Test"
            });
            data.Categories.Add(new Category
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                CategoryImage = "Test"
            });
            data.SaveChanges();

            var categoryService = new CategoryService(data);

            var result = await categoryService.AllCategoryAsync();

            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result, Is.TypeOf<List<Category>>());
        }
    }
}
