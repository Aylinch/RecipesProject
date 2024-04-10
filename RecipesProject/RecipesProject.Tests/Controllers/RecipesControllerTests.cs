using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RecipesProject.Areas.Admin.Controllers;
using RecipesProject.Data;
using RecipesProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using RecipesProject.Areas.Admin.Models;
using Moq;
using System.Threading;

namespace RecipesProject.Tests.Controllers
{
    public class RecipesControllerTests
    {
        [Test]
        public async Task Index_ReturnsAViewResult_WithAListOfUnapprovedRecipes()
        {
            #region Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "RecipesDatabase")
                .Options;
            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                var recipe = new Recipe
                {
                    CreatorId = "creatorId",
                    Title = "Sample Recipe",
                    Description = "Sample Description",
                    Instructions = "Sample Instructions"
                };

                context.Recipes.Add(recipe);
                context.SaveChanges();

                var controller = new RecipesController(context);
                #endregion
                #region Act
                var result = await controller.Index();
                #endregion
                #region Assert
                Assert.IsInstanceOf<ViewResult>(result);
                var viewResult = result as ViewResult;
                Assert.IsNotNull(viewResult.ViewData.Model);
                #endregion
            }
        }
        [Test]
        public async Task Index_ReturnsCorrectView()
        {
            #region Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "RecipesDatabase")
                .Options;
            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                var controller = new RecipesController(context);
                #endregion
                #region Act
                var result = await controller.Index();
                #endregion
                #region Assert
                Assert.IsInstanceOf<ViewResult>(result);
                var viewResult = result as ViewResult;
                Assert.AreEqual("Index", viewResult?.ViewName);
                #endregion
            }
        }
        [Test]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            #region Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "RecipesDatabase")
                .Options;
            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                var controller = new RecipesController(context);
                #endregion
                #region Act
                var result = await controller.Details(null);
                #endregion
                #region Assert
                Assert.IsInstanceOf<NotFoundResult>(result);
                #endregion
            }
        }

        [Test]
        public async Task Details_ReturnsNotFound_WhenRecipeNotFound()
        {
            #region Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "RecipesDatabase")
                .Options;
            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                var controller = new RecipesController(context);
                #endregion
                #region Act
                var result = await controller.Details(Guid.NewGuid()); 
                #endregion
                #region Assert
                Assert.IsInstanceOf<NotFoundResult>(result);
                #endregion
            }
        }

        [Test]
        public async Task Details_ReturnsViewResult_WithRecipe_WhenRecipeExists()
        {
            #region Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "RecipesDatabase")
                .Options;
            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                var sampleRecipe = new Recipe
                {
                    Id = Guid.NewGuid(),
                    Title = "Sample Recipe",
                    CreatorId = "sampleCreatorId",
                    Description = "Sample Description",
                    Instructions = "Sample Instructions"
                };
                context.Recipes.Add(sampleRecipe);
                context.SaveChanges();

                var controller = new RecipesController(context);
                #endregion
                #region Act
                var result = await controller.Details(sampleRecipe.Id);
                #endregion
                #region Assert
                Assert.IsInstanceOf<ViewResult>(result);
                var viewResult = result as ViewResult;
                Assert.IsNotNull(viewResult.ViewData.Model);
                var model = viewResult.ViewData.Model as Recipe;
                Assert.AreEqual(sampleRecipe.Id, model.Id);
                #endregion
            }
        }
        [Test]
        public async Task Create_ReturnsViewResult()
        {
            #region Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "RecipesDatabase")
                .Options;
            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                var controller = new RecipesController(context);
                #endregion
                #region Act
                var result = controller.Create();
                #endregion
                #region Assert
                Assert.IsInstanceOf<ViewResult>(result);
                #endregion
            }
        }

        [Test]
        public async Task Create_WithValidModel_ReturnsRedirectToActionResult()
        {
            #region Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "RecipesDatabase")
                .Options;
            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                var controller = new RecipesController(context);
                var recipe = new Recipe
                {
                    Title = "Test Recipe",
                    Description = "Test Description",
                    Instructions = "Test Instructions",
                    CreatorId = "testUserId", 
                };
                #endregion
                #region Act
                var result = await controller.Create(recipe);
                #endregion
                #region Assert
                Assert.IsInstanceOf<RedirectToActionResult>(result);
                var redirectToActionResult = result as RedirectToActionResult;
                Assert.AreEqual("Index", redirectToActionResult?.ActionName);
                #endregion
            }
        }
        [Test]
        public async Task Edit_ReturnsViewResult_WithEditRecipeViewModel_WhenRecipeExists()
        {
            #region Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "RecipesDatabase")
                .Options;
            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                var sampleRecipe = new Recipe
                {
                    Id = Guid.NewGuid(),
                    Title = "Sample Recipe",
                    Description = "Sample Description",
                    Instructions = "Sample Instructions",
                    CreatorId = "sample_creator_id"
                };
                context.Recipes.Add(sampleRecipe);
                await context.SaveChangesAsync();

                var controller = new RecipesController(context);
                #endregion
                #region Act
                var result = await controller.Edit(sampleRecipe.Id);
                #endregion
                #region Assert
                Assert.IsInstanceOf<ViewResult>(result);
                var viewResult = result as ViewResult;
                Assert.IsAssignableFrom<EditRecipeViewModel>(viewResult.ViewData.Model);
                #endregion
            }
        }
        [Test]
        public async Task Edit_WithValidModel_ReturnsRedirectToActionResult()
        {
            #region Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                var sampleRecipe = new Recipe
                {
                    Id = Guid.NewGuid(),
                    Title = "Sample Recipe",
                    CreatorId = "sample_creator_id",
                    Description = "Sample Description",
                    Instructions = "Sample Instructions"
                };
                context.Recipes.Add(sampleRecipe);
                await context.SaveChangesAsync();

                var controller = new RecipesController(context);
                var editViewModel = new EditRecipeViewModel
                {
                    Id = sampleRecipe.Id,
                    Title = "Updated Title",
                    Description = "Updated Description",
                    Instructions = "Updated Instructions",
                    CreatorId = "sample_creator_id",
                };
                #endregion
                #region Act
                var result = await controller.Edit(sampleRecipe.Id, editViewModel);
                #endregion
                #region Assert
                Assert.IsInstanceOf<RedirectToActionResult>(result);
                var redirectToActionResult = result as RedirectToActionResult;
                Assert.AreEqual("Index", redirectToActionResult?.ActionName);
                #endregion
            }
        }

        [Test]
        public async Task Edit_WithInvalidModel_ReturnsViewResult()
        {
        #region Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                var model = new EditRecipeViewModel
                {
                };
                var controller = new RecipesController(context);
                controller.ModelState.AddModelError("key", "error message");
                #endregion
                #region Act
                var result = await controller.Edit(Guid.NewGuid(), model);
                #endregion
                #region Assert
                Assert.That(result, Is.InstanceOf<ViewResult>());
                var viewResult = (ViewResult)result;
                Assert.AreEqual(model, viewResult.Model);
                #endregion
            }
        }

        [Test]
        public async Task Delete_ReturnsNotFound_WhenIdIsNull()
        {
            #region Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                var controller = new RecipesController(context);
                #endregion
                #region Act
                var result = await controller.Delete(null);
                #endregion
                #region Assert
                Assert.IsInstanceOf<NotFoundResult>(result);
                #endregion
            }
        }
        [Test]
        public async Task DeleteConfirmed_ReturnsRedirectToActionResult_WhenRecipeExists()
        {
            #region Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                var recipeId = Guid.NewGuid();
                var recipe = new Recipe
                {
                    Id = recipeId,
                    Title = "Sample Recipe",
                    CreatorId = "sampleCreatorId",
                    Description = "Sample Description",
                    Instructions = "Sample Instructions"
                };
                context.Recipes.Add(recipe);
                await context.SaveChangesAsync();

                var controller = new RecipesController(context);
                #endregion
                #region Act
                var result = await controller.DeleteConfirmed(recipeId);
                #endregion
                #region Assert
                Assert.IsInstanceOf<RedirectToActionResult>(result);
                var redirectToActionResult = result as RedirectToActionResult;
                Assert.AreEqual("Index", redirectToActionResult?.ActionName);
                var deletedRecipe = await context.Recipes.FindAsync(recipeId);
                Assert.IsNull(deletedRecipe);
                #endregion
            }
        }

        [Test]
        public async Task DeleteConfirmed_ReturnsNotFound_WhenRecipeNotFound()
        {
            #region Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                var controller = new RecipesController(context);
                #endregion
                #region Act
                var result = await controller.DeleteConfirmed(Guid.NewGuid());
                #endregion
                #region Assert
                Assert.IsInstanceOf<NotFoundResult>(result);
                #endregion
            }
        }
        [Test]
        public async Task ApproveRecipe_ReturnsRedirectToActionResult()
        {
            #region Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                var recipeId = Guid.NewGuid();
                var recipe = new Recipe
                {
                    Id = recipeId,
                    Title = "Sample Recipe",
                    CreatorId = "sampleCreatorId",
                    Description = "Sample Description",
                    Instructions = "Sample Instructions"
                };
                context.Recipes.Add(recipe);
                await context.SaveChangesAsync();

                var controller = new RecipesController(context);
                #endregion
                #region Act
                var result = await controller.ApproveRecipe(recipeId);
                #endregion
                #region Assert
                Assert.IsInstanceOf<RedirectToActionResult>(result);
                var redirectToActionResult = result as RedirectToActionResult;
                Assert.AreEqual("Index", redirectToActionResult?.ActionName);
                var approvedRecipe = await context.Recipes.FindAsync(recipeId);
                Assert.IsTrue(approvedRecipe.IsApproved);
                #endregion
            }
        }
        [Test]
        public async Task ApproveRecipe_ReturnsNotFound_WhenRecipeNotFound()
        {
            #region Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                var controller = new RecipesController(context);
                #endregion
                #region Act
                var result = await controller.ApproveRecipe(Guid.NewGuid());
                #endregion
                #region Assert
                Assert.IsInstanceOf<NotFoundResult>(result);
                #endregion
            }
        }
        [Test]
        public async Task DeleteConfirmed_RemovesRecipeAndRedirectsToIndex_WhenRecipeExists()
        {
            #region Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                var recipeId = Guid.NewGuid();
                var recipe = new Recipe
                {
                    Id = recipeId,
                    Title = "Sample Recipe",
                    CreatorId = "sampleCreatorId",
                    Description = "Sample Description",
                    Instructions = "Sample Instructions"
                };
                context.Recipes.Add(recipe);
                await context.SaveChangesAsync();

                var controller = new RecipesController(context);
                #endregion
                #region Act
                var result = await controller.DeleteConfirmed(recipeId);
                #endregion
                #region Assert
                Assert.IsInstanceOf<RedirectToActionResult>(result);
                var redirectToActionResult = result as RedirectToActionResult;
                Assert.AreEqual("Index", redirectToActionResult?.ActionName);
                var deletedRecipe = await context.Recipes.FindAsync(recipeId);
                Assert.IsNull(deletedRecipe);
                #endregion
            }
        }

        [Test]
        public async Task DeleteConfirmed_DoesNotSaveChanges_WhenRecipeNotFound()
        {
            #region Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                var controller = new RecipesController(context);
                #endregion
                #region Act
                await controller.DeleteConfirmed(Guid.NewGuid());
                #endregion
                #region Assert
                Assert.AreEqual(0, await context.SaveChangesAsync());
                #endregion
            }
        }
        [Test]
        public async Task Index_ReturnsEmptyList_WhenNoRecipesExist()
        {
            #region Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                var controller = new RecipesController(context);
                #endregion
                #region Act
                var result = await controller.Index();
                #endregion
                #region Assert
                Assert.IsInstanceOf<ViewResult>(result);
                var viewResult = result as ViewResult;
                Assert.IsNotNull(viewResult.ViewData.Model);
                var model = viewResult.ViewData.Model as List<Recipe>;
                Assert.IsEmpty(model);
                #endregion
            }
        }
    }
}