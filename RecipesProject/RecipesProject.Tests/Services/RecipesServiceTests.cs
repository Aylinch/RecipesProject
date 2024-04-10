using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RecipesProject.Data;
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
        [Test]
        public async Task AddRecipe_WithEmptyIngredientsList_SavesToDatabase()
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

        [Test]
        public void AddRecipesThrowsNullExceptions()
        {
            using var data = DatabaseMock.Instance;
            var RecipeService = new RecipeService(data);
            var ex = Assert.ThrowsAsync<NullReferenceException>(async ()
                => await RecipeService.AddRecipe(null!, null!));
        }
        [Test]
        public async Task TodaySpecial_ReturnsEmptyList_WhenNoRecipesAvailable()
        {
            #region Arrange
            using var data = DatabaseMock.Instance;
            var recipeService = new RecipeService(data);
            #endregion
            #region Act
            var todaySpecialRecipes = await recipeService.TodaySpacial();
            #endregion
            #region Assert
            Assert.IsEmpty(todaySpecialRecipes);
            #endregion
        }
        [Test]
        public async Task GetRecipe_CorrectRequest()
        {
            #region Arrange

            using var data = DatabaseMock.Instance;

            var recipeService = new RecipeService(data);
            var recipeId = Guid.NewGuid();

            data.Recipes.Add(new Recipe()
            {
                Id = recipeId,
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

            data.SaveChanges();

            #endregion

            #region Act

            var result = await recipeService.GetRecipeByIdAsync(recipeId);

            var recipe = await data.Recipes
                .Where(r => r.Id == recipeId)
                .Where(r => r.PrepTime == 10)
                .Where(r => r.Title == "TestTitle")
                .Where(r => r.TotalTime == 10)
                .FirstOrDefaultAsync();

            #endregion

            #region Assert

            Assert.That(recipe, Is.Not.Null);

            #endregion
        }

        [Test]
        public async Task ListRecipes_GetAllRecipesByGivenUserId()
        {
            using var data = DatabaseMock.Instance;
            var recipeService = new RecipeService(data);
            var userId = Guid.NewGuid();

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
                CreatorId = userId.ToString(),
            });

            data.SaveChanges();

            var result = await recipeService.GetUserRecipes(userId.ToString());

            Assert.That(result.Count(), Is.EqualTo(1));
        }
        [Test]
        public async Task GetRecipeByIdAsync_ReturnsNull_WhenRecipeNotFound()
        {
            #region Arrange
            using var data = DatabaseMock.Instance;
            var recipeService = new RecipeService(data);
            var nonExistingRecipeId = Guid.NewGuid();
            #endregion

            #region Act
            var result = await recipeService.GetRecipeByIdAsync(nonExistingRecipeId);
            #endregion
            #region Assert
            Assert.IsNull(result);
            #endregion
        }
        [Test]
        public async Task FilterAsync_ReturnsAllRecipes_WhenFilterModelIsNull()
        {
            #region Arrange
            using var data = DatabaseMock.Instance;
            var recipeService = new RecipeService(data);
            #endregion
            #region Act
            var filteredRecipes = await recipeService.FilterAsync(null);
            #endregion
            #region Assert
            var allRecipes = await recipeService.AllAsync();
            Assert.AreEqual(allRecipes.Count, filteredRecipes.Count);
            #endregion
        }
        [Test]
        public async Task FilterAsync_ReturnsFilteredRecipes()
        {
            #region Arrange
            using var data = DatabaseMock.Instance;
            var recipeService = new RecipeService(data);

            data.Recipes.AddRange(new List<Recipe>
{
    new Recipe
    {
        Id = Guid.NewGuid(),
        Title = "Title",
        Description = "Description",
        Instructions = "Instruction",
        Servings = 4,
        CookTime = 30,
        CreatorId = Guid.NewGuid().ToString(),
        RecipeIngredients = new HashSet<RecipeIngredients>
        {
            new RecipeIngredients
            {
                Ingredient = new Ingredient { Name = "product" },
                IngredientQuanitity = "500"
            },
            new RecipeIngredients
            {
                Ingredient = new Ingredient { Name = "Product" },
                IngredientQuanitity = "400"
            }
        }
    },
    new Recipe
    {
        Id = Guid.NewGuid(),
        Title = "Title",
        Description = "Description",
        Instructions = "Instruction",
        Servings = 2,
        CookTime = 25,
        CreatorId = Guid.NewGuid().ToString(),
        RecipeIngredients = new HashSet<RecipeIngredients>
        {
            new RecipeIngredients
            {
                Ingredient = new Ingredient { Name = "Product" },
                IngredientQuanitity = "400"
            },
            new RecipeIngredients
            {
                Ingredient = new Ingredient { Name = "Product" },
                IngredientQuanitity = "300"
            }
        }
      },
    });
            data.SaveChanges();

            var filterModel = new FilterViewModel
            {
                ServingsFilter = 4,
                IngredientFilter = "chicken",
                TimeFilter = "fast"
            };
            #endregion

            #region Act
            var filteredRecipes = await recipeService.FilterAsync(filterModel);
            #endregion

            #region Assert
            Assert.IsNotNull(filteredRecipes);
            #endregion
        }
        [Test]
        public async Task FilterAsync_ReturnsEmptyList_WhenNoRecipesMatchCriteria()
        {
            #region Arrange
            using var data = DatabaseMock.Instance;
            var recipeService = new RecipeService(data);
            var filterModel = new FilterViewModel
            {
                ServingsFilter = 10,
                IngredientFilter = "tomato",
                TimeFilter = "fast"
            };
            #endregion
            #region Act
            var filteredRecipes = await recipeService.FilterAsync(filterModel);
            #endregion
            #region Assert
            Assert.IsEmpty(filteredRecipes);
            #endregion
        }
        [Test]
        public async Task FilterAsync_ReturnsRecipesWithSpecifiedIngredient()
        {
            #region Arrange
            using var data = DatabaseMock.Instance;
            var recipeService = new RecipeService(data);
            data.Recipes.AddRange(new List<Recipe>
    {
        new Recipe
        {
            Id = Guid.NewGuid(),
            Title = "Title",
            Description = "Descripton",
            Instructions = "Instruction",
            Servings = 4,
            CookTime = 30,
            CreatorId = Guid.NewGuid().ToString(),
            RecipeIngredients = new HashSet<RecipeIngredients>
            {
                new RecipeIngredients
                {
                    Ingredient = new Ingredient { Name = "Product" },
                    IngredientQuanitity = "500"
                },
                new RecipeIngredients
                {
                    Ingredient = new Ingredient { Name = "Product" },
                    IngredientQuanitity = "400"
                }
            }
        },
    });
            data.SaveChanges();

            var filterModel = new FilterViewModel
            {
                ServingsFilter = 0,
                IngredientFilter = "chicken"
            };
            #endregion
            #region Act
            var filteredRecipes = await recipeService.FilterAsync(filterModel);
            #endregion
            #region Assert
            Assert.IsNotNull(filteredRecipes);
            #endregion
        }
        [Test]
        public async Task FilterAsync_ReturnsRecipesWithCorrectCookingTime()
        {
            #region Аrrange
            using var data = DatabaseMock.Instance;
            var recipeService = new RecipeService(data);
            data.Recipes.AddRange(new List<Recipe>
    {
        new Recipe
        {
            Id = Guid.NewGuid(),
            Title = "Tile",
            Description = "Description",
            Instructions = "Instruction",
            Servings = 4,
            CookTime = 30,
            CreatorId = Guid.NewGuid().ToString(),
            RecipeIngredients = new HashSet<RecipeIngredients>
            {
                new RecipeIngredients
                {
                    Ingredient = new Ingredient { Name = "Product" },
                    IngredientQuanitity = "500"
                },
                new RecipeIngredients
                {
                    Ingredient = new Ingredient { Name = "Product" },
                    IngredientQuanitity = "400"
                }
            }
        },
    });
            data.SaveChanges();

            var filterModel = new FilterViewModel
            {
                ServingsFilter = 0,
                TimeFilter = "medium" 
            };
            #endregion
            #region Act
            var filteredRecipes = await recipeService.FilterAsync(filterModel);
            #endregion
            #region Assert
            Assert.IsNotNull(filteredRecipes);
            #endregion
        }
        [Test]
        public async Task TodaySpecial_ReturnsUpToFourApprovedRecipes()
        {
            #region Arrange
            using var data = DatabaseMock.Instance;
            var recipeService = new RecipeService(data);
            data.Recipes.AddRange(new List<Recipe>
    {
        new Recipe
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
        },
        new Recipe
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
        },

    });
            data.SaveChanges();
            #endregion
            #region Act
            var todaySpecialRecipes = await recipeService.TodaySpacial();
            #endregion
            #region Assert
            Assert.IsNotNull(todaySpecialRecipes);
            Assert.IsTrue(todaySpecialRecipes.Count <= 4); 
            Assert.IsTrue(todaySpecialRecipes.All(recipe => data.Recipes.Any(r => r.Id == recipe.Id && r.IsApproved)));
            #endregion
        }
        [Test]
        public async Task TodaySpecial_ReturnsEmptyList_WhenNoApprovedRecipesAvailable()
        {
            # region Arrange
            using var data = DatabaseMock.Instance;
            var recipeService = new RecipeService(data);
            data.Recipes.AddRange(new List<Recipe>
    {
        new Recipe
        {
            Id = Guid.NewGuid(),
            Title = "UnapprovedRecipe",
            Description = "Unapproved Recipe Description",
            Instructions = "Unapproved Recipe Instructions",
            PrepTime = 10,
            CookTime = 10,
            TotalTime = 10,
            IsApproved = false,
            CategoryId = Guid.NewGuid(),
            Servings = 4,
            Image = "UnapprovedImage",
            CreatorId = Guid.NewGuid().ToString(),
        },
        new Recipe
        {
            Id = Guid.NewGuid(),
            Title = "AnotherUnapprovedRecipe",
            Description = "Another Unapproved Recipe Description",
            Instructions = "Another Unapproved Recipe Instructions",
            PrepTime = 10,
            CookTime = 10,
            TotalTime = 10,
            IsApproved = false,
            CategoryId = Guid.NewGuid(),
            Servings = 4,
            Image = "AnotherUnapprovedImage",
            CreatorId = Guid.NewGuid().ToString(),
        }
    });
            data.SaveChanges();
            #endregion
            #region Act
            var todaySpecialRecipes = await recipeService.TodaySpacial();
            #endregion
            #region Assert
            Assert.IsEmpty(todaySpecialRecipes);
            #endregion
        }

        [Test]
        public async Task FilterAsync_ReturnsFilteredRecipes_WhenModelNotNull()
        {
            #region Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                var recipes = new List<Recipe>
        {
            new Recipe { Title = "Recipe 1", Servings = 2, CookTime = 30, CreatorId = "sampleId", Description = "Sample Description", Instructions = "Sample Instructions" },
            new Recipe { Title = "Recipe 2", Servings = 4, CookTime = 40, CreatorId = "sampleId", Description = "Sample Description", Instructions = "Sample Instructions" },
            new Recipe { Title = "Recipe 3", Servings = 6, CookTime = 50, CreatorId = "sampleId", Description = "Sample Description", Instructions = "Sample Instructions" }
        };
                await context.Recipes.AddRangeAsync(recipes);
                await context.SaveChangesAsync();
                var service = new RecipeService(context);
                var filterModel = new FilterViewModel
                {
                    ServingsFilter = 4,
                    TimeFilter = "medium"
                };
                #endregion
                #region Act
                var result = await service.FilterAsync(filterModel);
                #endregion
                #region Assert
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual("Recipe 2", result[0].Title);
                #endregion
            }
        }
    }
    }
