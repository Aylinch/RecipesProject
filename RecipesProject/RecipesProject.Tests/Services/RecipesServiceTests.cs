using NUnit.Framework;
using RecipesProject.Data.Entities;
using RecipesProject.Models.RecipeViewModels;
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
    public class RecipesServiceTests
    {

        [Test]
        public async Task LoadAllRecipes_ReturnAllRecipes()
        {
            #region Arrange

            using var data = DatabaseMock.Instance;

            data.Recipes.Add(new Recipe()
            {
                Id = Guid.NewGuid(),
                Title = "TestTitle",
                Description = "TestDescription",
                Instructions = "TestInstruction",
                PrepTime = 10,
                CookTime = 10,
                TotalTime = 10,
                IsApproved = true,
                CategoryId = Guid.NewGuid(),
                Servings = 4,
                Image = "TestImage",
                CreatorId = Guid.NewGuid().ToString(),
            });
            data.Recipes.Add(new Recipe()
            {
                Id = Guid.NewGuid(),
                Title = "TestTitle2",
                Description = "TestDescription2",
                Instructions = "TestInstruction2",
                PrepTime = 10,
                CookTime = 10,
                TotalTime = 10,
                IsApproved = true,
                CategoryId = Guid.NewGuid(),
                Servings = 4,
                Image = "TestImage2",
                CreatorId = Guid.NewGuid().ToString(),
            });

            data.SaveChanges();

            var recipeService = new RecipeService(data);

            #endregion

            #region Act

            var result = await recipeService.AllAsync();

            #endregion

            #region Assert

            Assert.That(result.Count, Is.EqualTo(2));

            Assert.That(result, Is.TypeOf<List<RecipeViewModel>>());

            #endregion

        }
        [Test]
        public async Task AddRecipe_SavesToDatabase()
        {
            #region Arrange
            using var data = DatabaseMock.Instance;
            var recipeService = new RecipeService(data);

            var userId = Guid.NewGuid().ToString();
            var model = new AddRecipeViewModel()
            {
                Title = "NewRecipe",
                Description = "New Recipe Description",
                Instructions = "New Recipe Instructions",
                PrepTime = 15,
                CookTime = 20,
                TotalTime = 35,
                Servings = 4,
                Image = "NewRecipeImage.jpg",
                CategoryId = Guid.NewGuid(),
                Ingredients = new List<IngredientViewModel>()
        {
            new IngredientViewModel()
            {
                Id = Guid.NewGuid(),
                Name = "Ingredient1",
                Quantity = "100",
            },
            new IngredientViewModel()
            {
                Id = Guid.NewGuid(),
                Name = "Ingredient2",
                Quantity = "5",
            },

        }
            };
            #endregion

            #region Act
            await recipeService.AddRecipe(model, userId);
            #endregion

            #region Assert
            var addedRecipe = data.Recipes.FirstOrDefault(r => r.Title == "NewRecipe");
            Assert.IsNotNull(addedRecipe);
            #endregion
        }


    }
}
