
using Microsoft.AspNetCore.Mvc;
using Moq;
using Pictures.Controllers;
using Pictures.DAL.Interfaces;
using Pictures.Domain.Enums;
using Pictures.Domain.Responses;
using Pictures.Domain.ViewModels.Account;
using Pictures.Services.Interfaces;
using System.Security.Claims;
using Xunit;

namespace Pictures.UnitTests.Controllers
{
    public class AccountControllerTests
    {
        //Setup
        private readonly Mock<IAccountService> _accountServiceMock;

        public AccountControllerTests()
        {
            _accountServiceMock = new Mock<IAccountService>();
        }
        //Setup

        [Fact]
        public void Registration_GET_ExpectedRegistrationPageAsResult()
        {
            //Assert
            var accountController = new AccountController(_accountServiceMock.Object);

            //Act
            var result = accountController.Registration();

            //Assert
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public async Task Registration_POST_ExpectedRedirectToMyPicturesPage_WhenValidRegistrationViewModel()
        {
            //Assert
            var model = TestEntitiesProvider.GetRegistrationViewModel();
            var account = TestEntitiesProvider.GetAccount();
            var accountServiceResponse = new Response<ClaimsIdentity>
            {
                Data = TestEntitiesProvider.GetClaims(account),
                StatusCode = StatusCode.Success
            };

            _accountServiceMock.Setup(x => x.Register(model)).ReturnsAsync(accountServiceResponse);

            var accountController = TestEntitiesProvider.GetAccountControllerWithMockedAuthorization(_accountServiceMock);

            //Act
            var result = await accountController.Registration(model) as RedirectToActionResult;

            //Assert
            Assert.Equal("Picture", result.ControllerName);
            Assert.Equal("MyPictures", result.ActionName);
        }
        
        [Fact]
        public async Task Registration_POST_ExpectedReturnRegistrationViewModelToView_WhenInvalidRegistrationViewModel()
        {
            //Assert
            var model = TestEntitiesProvider.GetInvalidRegistrationViewModel();

            var accountController = new AccountController(_accountServiceMock.Object);
            accountController.ModelState.AddModelError("", "Invalid Model");

            //Act
            var result = await accountController.Registration(model) as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<RegistrationViewModel>(result.Model);
            Assert.Equal(model, result.Model);
        }

        [Fact]
        public async Task Registration_POST_ExpectedReturnRegistrationViewModelToView_WhenValidRegistrationViewModel_ButServiceResponseIsBad()
        {
            //Assert
            var model = TestEntitiesProvider.GetRegistrationViewModel();

            var accountServiceResponse = new Response<ClaimsIdentity>
            {
                Description = "The specified login exist",
                StatusCode = StatusCode.SpecifiedDataExist
            };

            _accountServiceMock.Setup(x => x.Register(model)).ReturnsAsync(accountServiceResponse);
            var accountController = new AccountController(_accountServiceMock.Object);

            //Act
            var result = await accountController.Registration(model) as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<RegistrationViewModel>(result.Model);
            Assert.Equal(model, result.Model);
            _accountServiceMock.Verify(x => x.Register(model), Times.Once);
        }

        [Fact]
        public void Login_GET_ExpectedLoginPageAsResult()
        {
            //Assert
            var accountController = new AccountController(_accountServiceMock.Object);

            //Act
            var result = accountController.Login();

            //Assert
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public async Task Login_POST_ExpectedRedirectToMyPicturesPage_WhenValidLoginViewModel()
        {
            //Assert
            var model = TestEntitiesProvider.GetLoginViewModel();
            var account = TestEntitiesProvider.GetAccount();
            var accountServiceResponse = new Response<ClaimsIdentity>
            {
                Data = TestEntitiesProvider.GetClaims(account),
                StatusCode = StatusCode.Success
            };

            _accountServiceMock.Setup(x => x.Login(model)).ReturnsAsync(accountServiceResponse);

            var accountController = TestEntitiesProvider.GetAccountControllerWithMockedAuthorization(_accountServiceMock);

            //Act
            var result = await accountController.Login(model) as RedirectToActionResult;

            //Assert
            Assert.Equal("Picture", result.ControllerName);
            Assert.Equal("MyPictures", result.ActionName);
        }

        [Fact]
        public async Task Login_POST_ExpectedReturnRegistrationViewModelToView_WhenInvalidLoginViewModel()
        {
            //Assert
            var model = TestEntitiesProvider.GetInvalidLoginViewModel();

            var accountController = new AccountController(_accountServiceMock.Object);
            accountController.ModelState.AddModelError("", "Invalid Model");

            //Act
            var result = await accountController.Login(model) as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<LoginViewModel>(result.Model);
            Assert.Equal(model, result.Model);
        }

        [Fact]
        public async Task Login_POST_ExpectedReturnRegistrationViewModelToView_WhenValidLoginViewModel_ButServiceResponseIsBad()
        {
            //Assert
            var model = TestEntitiesProvider.GetLoginViewModel();

            var accountServiceResponse = new Response<ClaimsIdentity>
            {
                Description = "Account not found",
                StatusCode = StatusCode.NotFound
            };

            _accountServiceMock.Setup(x => x.Login(model)).ReturnsAsync(accountServiceResponse);
            var accountController = new AccountController(_accountServiceMock.Object);

            //Act
            var result = await accountController.Login(model) as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<LoginViewModel>(result.Model);
            Assert.Equal(model, result.Model);
            _accountServiceMock.Verify(x => x.Login(model), Times.Once);
        }

        [Fact]
        public async Task Logout_ExpectedRedirectToIndexPage()
        {
            //Assert
            var accountController = TestEntitiesProvider.GetAccountControllerWithMockedAuthorization(_accountServiceMock);

            //Act
            var result = await accountController.Logout() as RedirectToActionResult;

            //Assert
            Assert.Equal("Home", result.ControllerName);
            Assert.Equal("Index", result.ActionName);
        }
    }
}
