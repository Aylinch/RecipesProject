using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RecipesProject.Contracts;
using RecipesProject.Controllers;
using RecipesProject.Models;
using RecipesProject.Models.RecipeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesProject.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        [Test]
        public async Task Index_ReturnsViewResultWithModel()
        {
            #region Arrange
            var mockRecipeService = new Mock<IRecipeService>();
            var expectedModel = new List<RecipeViewModel>();
            mockRecipeService.Setup(service => service.TodaySpacial()).ReturnsAsync(expectedModel);
            var controller = new HomeController(null, mockRecipeService.Object);
            #endregion
            #region Act
            var result = await controller.Index();
            #endregion
            #region Assert
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = (ViewResult)result;
            Assert.AreEqual(expectedModel, viewResult.Model);
            #endregion
        }

        [Test]
        public void Privacy_ReturnsViewResult()
        {
            #region Arrange
            var controller = new HomeController(null, null);
            #endregion
            #region Act
            var result = controller.Privacy();
            #endregion
            #region Assert
            Assert.IsInstanceOf<ViewResult>(result);
            #endregion
        }

        [Test]
        public void Error_ReturnsViewResultWithRequestId()
        {
            #region Arrange
            var httpContext = new DefaultHttpContext();
            httpContext.TraceIdentifier = "TestTraceIdentifier";

            var controller = new HomeController(null, null)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = httpContext
                }
            };
            #endregion
            #region Act
            var result = controller.Error();
            #endregion
            #region Assert
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = (ViewResult)result;
            Assert.IsInstanceOf<ErrorViewModel>(viewResult.Model);
            var model = (ErrorViewModel)viewResult.Model;
            Assert.AreEqual("TestTraceIdentifier", model.RequestId);
            #endregion
        }
    [Test]
    public async Task Index_ReturnsViewResult_WithModel()
    {
            #region Arrange
            var mockRecipeService = new Mock<IRecipeService>();
        var expectedModel = new List<RecipeViewModel>();
        mockRecipeService.Setup(service => service.TodaySpacial()).ReturnsAsync(expectedModel);
        var controller = new HomeController(null, mockRecipeService.Object);
            #endregion
            #region Act
            var result = await controller.Index();
            #endregion
            #region Assert
            Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = (ViewResult)result;
        Assert.AreEqual(expectedModel, viewResult.Model);
            #endregion
        }

        [Test]
    public async Task Index_ReturnsViewResult_WhenRecipeServiceReturnsNull()
    {
            #region Arrange
            var mockRecipeService = new Mock<IRecipeService>();
        mockRecipeService.Setup(service => service.TodaySpacial()).ReturnsAsync((List<RecipeViewModel>)null);
        var controller = new HomeController(null, mockRecipeService.Object);
            #endregion
            #region Act
            var result = await controller.Index();
            #endregion
            #region Assert
            Assert.IsInstanceOf<ViewResult>(result);
            #endregion
        }

        [Test]
    public void Error_ReturnsViewResult_WithNonEmptyRequestId()
    {
           #region Arrange
            var httpContext = new DefaultHttpContext();
        httpContext.TraceIdentifier = "TestTraceIdentifier";

        var controller = new HomeController(null, null)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            }
        };
            #endregion
            #region Act
            var result = controller.Error();
            #endregion
            #region Assert
            Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = (ViewResult)result;
        Assert.IsInstanceOf<ErrorViewModel>(viewResult.Model);
        var model = (ErrorViewModel)viewResult.Model;
        Assert.IsNotNull(model.RequestId);
        Assert.IsNotEmpty(model.RequestId);
            #endregion
        }
        [Test]
        public void Error_ReturnsViewResult_WithNonNullRequestId_WhenHttpContextIsNull()
        {
            #region Arrange
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.SetupGet(h => h.TraceIdentifier).Returns("TestTraceIdentifier");

            var controller = new HomeController(null, null)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object
                }
            };
            #endregion
            #region Act
            var result = controller.Error();
            #endregion
            #region Assert
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = (ViewResult)result;
            Assert.IsInstanceOf<ErrorViewModel>(viewResult.Model);
            var model = (ErrorViewModel)viewResult.Model;
            Assert.IsNotNull(model.RequestId);
            #endregion
        }
    }
}
