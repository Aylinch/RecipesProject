using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using RecipesProject.Contracts;
using RecipesProject.Controllers;
using RecipesProject.Data.Entities;
using RecipesProject.Models;
using RecipesProject.Models.RecipeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RecipesProject.Tests.Controllers
{
    [TestFixture]
    public class RecipeControllerTests
    {
        [Test]
        public async Task AllRecipes_ReturnsViewResultWithRecipes()
        {
            #region Arrange
            var mockRecipeService = new Mock<IRecipeService>();
            var recipes = new List<RecipeViewModel> { new RecipeViewModel(), new RecipeViewModel() };
            mockRecipeService.Setup(service => service.AllAsync()).ReturnsAsync(recipes);
            var controller = new RecipeController(mockRecipeService.Object, null);
            #endregion
            #region Act
            var result = await controller.AllRecipes();
            #endregion
            #region Assert
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = (ViewResult)result;
            Assert.AreEqual(recipes, viewResult.Model);
            #endregion
        }
        [Test]
        public async Task GetUserRecipes_ReturnsViewResultWithUserRecipes()
        {
            #region Arrange
            var userId = "testUserId";
            var mockUser = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            }));
            var mockRecipeService = new Mock<IRecipeService>();
            var recipes = new List<RecipeViewModel> { new RecipeViewModel(), new RecipeViewModel() };
            mockRecipeService.Setup(service => service.GetUserRecipes(userId)).ReturnsAsync(recipes);
            var controller = new RecipeController(mockRecipeService.Object, null)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = mockUser }
                }
            };
            #endregion
            #region Act
            var result = await controller.GetUserRecipes();
            #endregion
            #region Assert
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = (ViewResult)result;
            Assert.AreEqual("UserRecipes", viewResult.ViewName);
            Assert.AreEqual(recipes, viewResult.Model);
            #endregion
        }
        [Test]
        public async Task AddRecipe_Get_ReturnsViewResultWithCategories()
        {
            #region Arrange
            var mockCategoryService = new Mock<ICategoryService>();
            var categories = new List<Category> { new Category(), new Category() };
            mockCategoryService.Setup(service => service.AllCategoryAsync()).ReturnsAsync(categories);
            var controller = new RecipeController(null, mockCategoryService.Object);
            #endregion
            #region Act
            var result = await controller.AddRecipe();
            #endregion
            #region Assert
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = (ViewResult)result;
            Assert.IsNotNull(viewResult.ViewData["CategoryId"]);
            Assert.IsInstanceOf<SelectList>(viewResult.ViewData["CategoryId"]);
            #endregion
        }
        [Test]
        public async Task GetRecipeById_ReturnsNotFoundResult_WhenRecipeNotFound()
        {
            #region Arrange
            var mockRecipeService = new Mock<IRecipeService>();
            mockRecipeService.Setup(service => service.GetRecipeByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Recipe)null);
            var controller = new RecipeController(mockRecipeService.Object, null);
            #endregion
            #region Act
            var result = await controller.GetRecipeById(Guid.NewGuid());
            #endregion
            #region Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
            #endregion
        }
        [Test]
        public async Task Filter_ReturnsViewResultWithFilteredRecipes()
        {
            #region Arrange
            var mockRecipeService = new Mock<IRecipeService>();
            var filteredRecipes = new List<RecipeViewModel>();
            mockRecipeService.Setup(service => service.FilterAsync(It.IsAny<FilterViewModel>())).ReturnsAsync(filteredRecipes);
            var controller = new RecipeController(mockRecipeService.Object, null);
            var model = new FilterViewModel();
            #endregion
            #region Act
            var result = await controller.Filter(model);
            #endregion
            #region Assert
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = (ViewResult)result;
            Assert.AreEqual("AllRecipes", viewResult.ViewName);
            Assert.AreEqual(filteredRecipes, viewResult.Model);
            #endregion
        }
        [Test]
        public async Task AddRecipe_Post_ReturnsRedirectToActionResult_WhenModelStateIsInvalid()
        {
            #region Arrange
            var mockRecipeService = new Mock<IRecipeService>();
            var mockCategoryService = new Mock<ICategoryService>();
            var controller = new RecipeController(mockRecipeService.Object, mockCategoryService.Object);
            controller.ModelState.AddModelError("PropertyName", "Error message");
            var model = new AddRecipeViewModel();
            #endregion
            #region Act
            var result = await controller.AddRecipe(model);
            #endregion
            #region Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            var redirectResult = (RedirectToActionResult)result;
            Assert.AreEqual("AllRecipes", redirectResult.ActionName);
            #endregion
        }
        [Test]
        public async Task AddRecipe_Post_ReturnsRedirectToActionResult_WhenModelIsValid()
        {
            #region Arrange
            var mockRecipeService = new Mock<IRecipeService>();
            var model = new AddRecipeViewModel();
            var controller = new RecipeController(mockRecipeService.Object, null);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "userId123")
            }, "mock"));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
            #endregion
            #region Act
            var result = await controller.AddRecipe(model);
            #endregion
            #region Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            var redirectToActionResult = (RedirectToActionResult)result;
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
            Assert.AreEqual("Home", redirectToActionResult.ControllerName);
            #endregion
        }
        [Test]
        public async Task RemoveFilters_ReturnsRedirectToActionResult_RedirectsToAllRecipesAction()
        {
            #region Arrange
            var controller = new RecipeController(null, null);
            #endregion
            #region Act
            var result = await controller.RemoveFilters(new FilterViewModel());
            #endregion
            #region Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            var redirectToActionResult = (RedirectToActionResult)result;
            Assert.AreEqual("AllRecipes", redirectToActionResult.ActionName);
            #endregion
        }

        [Test]
        public async Task RemoveFilters_ResetsFilterProperties()
        {
            #region Arrange
            var controller = new RecipeController(null, null);
            var model = new FilterViewModel
            {
                ServingsFilter = 2,
                IngredientFilter = "Test Ingredient",
                TimeFilter = "Test Time"
            };
            #endregion
            #region Act
            var result = await controller.RemoveFilters(model);
            #endregion
            #region Assert
            Assert.AreEqual(0, model.ServingsFilter);
            Assert.AreEqual("", model.IngredientFilter);
            Assert.AreEqual("", model.TimeFilter);
            #endregion
        }
    }
}