using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RecipesProject.Controllers;
using RecipesProject.Models;
using System.Security.Claims;
using RecipesProject.Data.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace RecipesProject.Tests.Controllers
{
    [TestFixture]
    public class AccountControllerTests
    {
        [Test]
        public async Task EditProfile_GET_ReturnsViewWithModel()
        {
            #region Arrange
            var userStoreMock = new Mock<IUserStore<User>>();
            var userManagerMock = new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

            var currentUserId = "testUserId";
            var currentUser = new User
            {
                Id = currentUserId,
                FirstName = "Name",
                LastName = "LastName",
                Email = "name@example.com",
                UserName = "name.last",
                Age = 30
            };

            var userManagerFindByIdResult = Task.FromResult(currentUser);
            userManagerMock.Setup(m => m.FindByIdAsync(currentUserId)).Returns(userManagerFindByIdResult);
            var controller = new AccountController(userManagerMock.Object, null, null);
            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, currentUserId),
                new Claim(ClaimTypes.Name, "name.last"),
            }));
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            };
            #endregion
            #region Act
            var result = await controller.EditProfile();
            #endregion
            #region Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            var viewModel = viewResult.Model as MyProfileViewModel;
            Assert.IsNotNull(viewModel);
            Assert.AreEqual(currentUser.FirstName, viewModel.FirstName);
            Assert.AreEqual(currentUser.LastName, viewModel.LastName);
            Assert.AreEqual(currentUser.Email, viewModel.Email);
            Assert.AreEqual(currentUser.UserName, viewModel.UserName);
            Assert.AreEqual(currentUser.Age, viewModel.Age);
            #endregion
        }
        [Test]
        public async Task EditProfile_POST_ValidModel_RedirectsToIndex()
        {
            #region Arrange
            var userStoreMock = new Mock<IUserStore<User>>();
            var userManagerMock = new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var claimsFactoryMock = new Mock<IUserClaimsPrincipalFactory<User>>();
            var optionsAccessorMock = new Mock<IOptions<IdentityOptions>>();
            var loggerMock = new Mock<ILogger<SignInManager<User>>>();
            var schemesMock = new Mock<IAuthenticationSchemeProvider>();
            var confirmationMock = new Mock<IUserConfirmation<User>>();

            var signInManagerMock = new Mock<SignInManager<User>>(
                userManagerMock.Object,
                httpContextAccessorMock.Object,
                claimsFactoryMock.Object,
                optionsAccessorMock.Object,
                loggerMock.Object,
                schemesMock.Object,
                confirmationMock.Object
            );

            var model = new MyProfileViewModel
            {
                FirstName = "Name",
                LastName = "Last",
                Email = "name@example.com",
                UserName = "name.last",
                Age = 30
            };

            var currentUser = new User
            {
                Id = "testUserId",
                FirstName = "Existing",
                LastName = "User",
                Email = "existing.user@example.com",
                UserName = "existinguser",
                Age = 25
            };
            var userManagerFindByIdResult = Task.FromResult(currentUser);
            userManagerMock.Setup(m => m.FindByIdAsync(It.IsAny<string>())).Returns(userManagerFindByIdResult);
            userManagerMock.Setup(m => m.UpdateAsync(currentUser)).ReturnsAsync(IdentityResult.Success);
            var controller = new AccountController(userManagerMock.Object, signInManagerMock.Object, null);
            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "testUserId"),
                new Claim(ClaimTypes.Name, "existinguser"),
            }));
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            };
            #endregion
            #region Act
            var result = await controller.EditProfile(model);
            #endregion
            #region Assert
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
            Assert.AreEqual("Home", redirectToActionResult.ControllerName);
            #endregion
        }

        [Test]
        public async Task EditProfile_POST_UnsuccessfulUpdate_ReturnsViewWithError()
        {
            #region Arrange
            var userStoreMock = new Mock<IUserStore<User>>();
            var userManagerMock = new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            var currentUser = new User
            {
                Id = "testUserId",
                FirstName = "Existing",
                LastName = "User",
                Email = "existing.user@example.com",
                UserName = "existinguser",
                Age = 25
            };
            var userManagerFindByIdResult = Task.FromResult(currentUser);
            userManagerMock.Setup(m => m.FindByIdAsync(It.IsAny<string>())).Returns(userManagerFindByIdResult);
            var errorDescription = "Error updating user.";
            userManagerMock.Setup(m => m.UpdateAsync(currentUser)).ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = errorDescription }));

            var signInManagerMock = new Mock<SignInManager<User>>(
                userManagerMock.Object,
                new HttpContextAccessor(),
                new Mock<IUserClaimsPrincipalFactory<User>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<User>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object,
                new Mock<IUserConfirmation<User>>().Object
            );
            var controller = new AccountController(userManagerMock.Object, signInManagerMock.Object, null);
            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
        new Claim(ClaimTypes.NameIdentifier, "testUserId"),
        new Claim(ClaimTypes.Name, "existinguser"),
            }));
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            };
            #endregion
            #region Act
            var result = await controller.EditProfile(new MyProfileViewModel());
            #endregion
            #region Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.IsTrue(viewResult.ViewData.ModelState.ErrorCount > 0);
            Assert.IsTrue(viewResult.ViewData.ModelState.ContainsKey(string.Empty));
            Assert.AreEqual(errorDescription, viewResult.ViewData.ModelState[string.Empty].Errors[0].ErrorMessage);
            #endregion
        }
        [Test]
        public async Task EditProfile_POST_InvalidModel_ReturnsViewWithError()
        {
            #region Arrange
            var userStoreMock = new Mock<IUserStore<User>>();
            var userManagerMock = new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            var signInManagerMock = new Mock<SignInManager<User>>(
                userManagerMock.Object,
                new HttpContextAccessor(),
                new Mock<IUserClaimsPrincipalFactory<User>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<User>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object,
                new Mock<IUserConfirmation<User>>().Object
            );

            var controller = new AccountController(userManagerMock.Object, signInManagerMock.Object, null);
            controller.ModelState.AddModelError("FirstName", "The FirstName field is required.");

            var model = new MyProfileViewModel();
            #endregion
            #region Act
            var result = await controller.EditProfile(model);
            #endregion
            #region Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);

            var viewResult = result as ViewResult;
            Assert.IsFalse(viewResult.ViewData.ModelState.IsValid);
            #endregion
        }
    }
}