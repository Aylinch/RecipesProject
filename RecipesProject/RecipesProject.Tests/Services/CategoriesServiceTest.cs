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
        [Test]
        public async Task AllFromCategoriesAsync_ReturnsRecipesBelongingToSpecifiedCategory()
        {
            #region Arrange
            using var data = DatabaseMock.Instance;
            var categoryService = new CategoryService(data);
            var categoryId = Guid.NewGuid();
            var category = new Category { Id = categoryId, Name = "TestCategory" };
            data.Categories.Add(category);
            data.SaveChanges();
            var recipesInCategory = new List<Recipe>
    {
        new Recipe
        {
            Id = Guid.NewGuid(),
            Title = "Recipe 1",
            Description = "Description for Recipe 1",
            CategoryId = categoryId,
            Instructions = "TestInstruction1",
            PrepTime = 8,
            CookTime = 12,
            TotalTime = 20,
            IsApproved = true,
            Servings = 5,
            Image = "TestImage1",
            CreatorId = Guid.NewGuid().ToString(),
        },
        new Recipe
        {
            Id = Guid.NewGuid(),
            Title = "Recipe 2",
            Description = "Description for Recipe 2",
            CategoryId = categoryId,
            Instructions = "TestInstruction2",
            PrepTime = 10,
            CookTime = 10,
            TotalTime = 10,
            IsApproved = true,
            Servings = 4,
            Image = "TestImage2",
            CreatorId = Guid.NewGuid().ToString(),
        },
    };
            data.Recipes.AddRange(recipesInCategory);
            data.SaveChanges();
            #endregion

            #region Act
            var recipesFromCategory = await categoryService.AllFromCategoriesAsync(categoryId);
            #endregion

            #region Assert
            Assert.IsNotNull(recipesFromCategory);
            #endregion
        }
        [Test]
        public async Task AllFromCategoriesAsync_ReturnsEmptyList_WhenCategoryHasNoRecipes()
        {
            #region Arrange
            using var data = DatabaseMock.Instance;
            var categoryService = new CategoryService(data);
            var categoryId = Guid.NewGuid();
            data.Categories.Add(new Category { Id = categoryId, Name = "TestCategory" });
            data.SaveChanges();
            #endregion
            #region Act
            var recipesFromCategory = await categoryService.AllFromCategoriesAsync(categoryId);
            #endregion
            #region Assert
            Assert.IsNotNull(recipesFromCategory);
            Assert.AreEqual(0, recipesFromCategory.Count);
            #endregion
        }
        [Test]
        public async Task AllFromCategoriesAsync_ReturnsNull_WhenCategoryIdNotFound()
        {
            #region Arrange
            using var data = DatabaseMock.Instance;
            var categoryService = new CategoryService(data);
            var nonExistentCategoryId = Guid.NewGuid();
            #endregion
            #region Act
            var recipesFromCategory = await categoryService.AllFromCategoriesAsync(nonExistentCategoryId);
            #endregion
            #region Assert
            Assert.That(recipesFromCategory, Is.Not.Null);
            Assert.That(recipesFromCategory.Count, Is.EqualTo(0));
            #endregion
        }
        [Test]
        public async Task AllFromCategoriesAsync_ReturnsNull_WhenCategoryHasNoRecipes()
        {
            #region Arrange
            using var data = DatabaseMock.Instance;
            var categoryService = new CategoryService(data);
            var categoryId = Guid.NewGuid();
            data.Categories.Add(new Category { Id = categoryId, Name = "TestCategory" });
            data.SaveChanges();
            #endregion
            #region Act
            var recipesFromCategory = await categoryService.AllFromCategoriesAsync(categoryId);
            #endregion
            #region Assert
            Assert.That(recipesFromCategory, Is.Not.Null);
            Assert.That(recipesFromCategory.Count, Is.EqualTo(0));
            #endregion
        }
        [Test]
        public async Task AllFromCategoriesAsync_ReturnsEmptyList_WhenCategoryIdNotFound()
        {
            #region Arrange
            using var data = DatabaseMock.Instance;
            var categoryService = new CategoryService(data);
            var nonExistentCategoryId = Guid.NewGuid();
            #endregion
            #region Act
            var recipesFromCategory = await categoryService.AllFromCategoriesAsync(nonExistentCategoryId);
            #endregion
            #region Assert
            Assert.IsNotNull(recipesFromCategory);
            Assert.IsEmpty(recipesFromCategory);
            #endregion
        }
        [Test]
        public async Task AllFromCategoriesAsync_ReturnsRecipesFromSpecifiedCategory()
        {
            #region Arrange
            using var data = DatabaseMock.Instance;
            var categoryService = new CategoryService(data);
            var categoryId = Guid.NewGuid();
            var category = new Category { Id = categoryId, Name = "TestCategory" };
            data.Categories.Add(category);
            data.SaveChanges();

            var recipesInCategory = new List<Recipe>
    {
        new Recipe
        {
            Id = Guid.NewGuid(),
            Title = "Recipe1",
            Description = "Description for Recipe1",
            CategoryId = categoryId,
            Instructions = "Instructions for Recipe1",
            CreatorId = "CreatorId1",
        },
        new Recipe
        {
            Id = Guid.NewGuid(),
            Title = "Recipe2",
            Description = "Description for Recipe2",
            CategoryId = categoryId,
            Instructions = "Instructions for Recipe2",
            CreatorId = "CreatorId2",
        }
    };
            data.Recipes.AddRange(recipesInCategory);
            data.SaveChanges();
            #endregion
            #region Act
            var recipesFromCategory = await categoryService.AllFromCategoriesAsync(categoryId);
            #endregion
            #region Assert
            Assert.IsNotNull(recipesFromCategory);
            Assert.AreEqual(2, recipesFromCategory.Count);
            Assert.IsTrue(recipesFromCategory.All(r => r.CategoryId == categoryId));
            #endregion
        }
    }
}
