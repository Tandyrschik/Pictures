
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Pictures.Controllers;
using Pictures.Domain.Entities;
using Pictures.Domain.Enums;
using Pictures.Domain.Responses;
using Pictures.Services.Interfaces;
using System.Security.Claims;
using Xunit;
using Pictures.Domain.ViewModels.Picture;

namespace Pictures.UnitTests.Controllers
{
    public class PictureControllerTests
    {
        //Setup
        private readonly Mock<IAccountService> _accountServiceMock;
        private readonly Mock<IPictureService> _pictureServiceMock;
        private readonly Mock<IFileManagerService> _fileManagerServiceMock;
        private readonly Mock<IWebHostEnvironment> _webHostEnviromentMock;

        public PictureControllerTests()
        {
            _accountServiceMock = new Mock<IAccountService>();
            _pictureServiceMock = new Mock<IPictureService>();
            _fileManagerServiceMock = new Mock<IFileManagerService>();
            _webHostEnviromentMock = TestEntitiesProvider.GetMockedWebHostEnvironmentWithMockedWebRootPath();
        }
        //Setup

        [Fact]
        public async Task MyPictures_GET_ExpectedPicturesListOfOneAccountInView_WhenServerResponseIsSuccess()
        {
            //Assert
            var model = TestEntitiesProvider.GetLoginViewModel();
            var account = TestEntitiesProvider.GetAccount();
            var pictureServiceResponse = new Response<IEnumerable<Picture>>
            {
                Data = TestEntitiesProvider.GetPicturesListOfOneAccount(),
                StatusCode = StatusCode.Success
            };

            var accountServiceResponse = new Response<ClaimsIdentity>
            {
                Data = TestEntitiesProvider.GetClaims(account),
                StatusCode = StatusCode.Success
            };

            _pictureServiceMock.Setup(x => x.GetPicturesOfAccount(account.Id)).ReturnsAsync(pictureServiceResponse);
            _accountServiceMock.Setup(x => x.GetAccountByLogin(account.Login)).ReturnsAsync(account);

            var pictureController = TestEntitiesProvider.GetPictureControllerWithMockedAuthorization
                (_pictureServiceMock, _accountServiceMock, _fileManagerServiceMock, _webHostEnviromentMock);

            pictureController.TempData = TestEntitiesProvider.GetMockedTempData(pictureController);

            //Act
            var result = await pictureController.MyPictures() as ViewResult;

            //Assert
            Assert.IsType<List<Picture>>(result.Model);
            Assert.Equal(pictureServiceResponse.Data, result.Model);

            _pictureServiceMock.Verify(x => x.GetPicturesOfAccount(account.Id), Times.Once());
            _accountServiceMock.Verify(x => x.GetAccountByLogin(account.Login), Times.Once());
        }

        [Fact]
        public async Task MyPictures_GET_ExpectedRedirectToAddPicturePage_WhenAccountHasNoOnePicture()
        {
            //Assert
            var model = TestEntitiesProvider.GetLoginViewModel();
            var account = TestEntitiesProvider.GetAccount();
            var pictureServiceResponse = new Response<IEnumerable<Picture>>
            {
                Data = new List<Picture>() { },
                StatusCode = StatusCode.NotFound
            };

            var accountServiceResponse = new Response<ClaimsIdentity>
            {
                Data = TestEntitiesProvider.GetClaims(account),
                StatusCode = StatusCode.Success
            };

            _pictureServiceMock.Setup(x => x.GetPicturesOfAccount(account.Id)).ReturnsAsync(pictureServiceResponse);
            _accountServiceMock.Setup(x => x.GetAccountByLogin(account.Login)).ReturnsAsync(account);

            var pictureController = TestEntitiesProvider.GetPictureControllerWithMockedAuthorization
                (_pictureServiceMock, _accountServiceMock, _fileManagerServiceMock, _webHostEnviromentMock);

            pictureController.TempData = TestEntitiesProvider.GetMockedTempData(pictureController);

            //Act
            var result = await pictureController.MyPictures() as RedirectToActionResult;

            //Assert
            Assert.Equal("Picture", result.ControllerName);
            Assert.Equal("AddPicture", result.ActionName);

            _pictureServiceMock.Verify(x => x.GetPicturesOfAccount(account.Id), Times.Once());
            _accountServiceMock.Verify(x => x.GetAccountByLogin(account.Login), Times.Once());
        }

        [Fact]
        public async Task MyPictures_GET_ExpectedRedirectToErrorPage()
        {
            //Assert
            var model = TestEntitiesProvider.GetLoginViewModel();
            var account = TestEntitiesProvider.GetAccount();
            var pictureServiceResponse = new Response<IEnumerable<Picture>>
            {
                Data = new List<Picture>() { },
                StatusCode = StatusCode.ServerError
            };

            var accountServiceResponse = new Response<ClaimsIdentity>
            {
                Data = TestEntitiesProvider.GetClaims(account),
                StatusCode = StatusCode.Success
            };

            _pictureServiceMock.Setup(x => x.GetPicturesOfAccount(account.Id)).ReturnsAsync(pictureServiceResponse);
            _accountServiceMock.Setup(x => x.GetAccountByLogin(account.Login)).ReturnsAsync(account);

            var pictureController = TestEntitiesProvider.GetPictureControllerWithMockedAuthorization
                (_pictureServiceMock, _accountServiceMock, _fileManagerServiceMock, _webHostEnviromentMock);

            pictureController.TempData = TestEntitiesProvider.GetMockedTempData(pictureController);

            //Act
            var result = await pictureController.MyPictures() as RedirectToActionResult;

            //Assert
            Assert.Equal("Error", result.ActionName);

            _pictureServiceMock.Verify(x => x.GetPicturesOfAccount(account.Id), Times.Once());
            _accountServiceMock.Verify(x => x.GetAccountByLogin(account.Login), Times.Once());
        }

        [Fact]
        public async Task RemovePicture_POST_ExpectedRedirectToMyPicturesPage()
        {
            //Arrange
            int pictureId = 0;
            var pictureServiceResponse = new Response<bool>
            {
                Data = true,
                StatusCode = StatusCode.Success
            };

            _pictureServiceMock.Setup(x => x.RemovePicture(pictureId)).ReturnsAsync(pictureServiceResponse);

            var pictureController = TestEntitiesProvider.GetPictureControllerWithMockedAuthorization
                (_pictureServiceMock, _accountServiceMock, _fileManagerServiceMock, _webHostEnviromentMock);

            //Act
            var result = await pictureController.RemovePicture(pictureId) as RedirectToActionResult;

            //Assert
            Assert.Equal("Picture", result.ControllerName);
            Assert.Equal("MyPictures", result.ActionName);

            _pictureServiceMock.Verify(x => x.RemovePicture(pictureId), Times.Once());
        }

        [Fact]
        public async Task RemovePicture_POST_ExpectedRedirectToErrorPage()
        {
            //Arrange
            int pictureId = 0;
            var pictureServiceResponse = new Response<bool>
            {
                Data = false,
                StatusCode = StatusCode.ServerError
            };

            _pictureServiceMock.Setup(x => x.RemovePicture(pictureId)).ReturnsAsync(pictureServiceResponse);

            var pictureController = TestEntitiesProvider.GetPictureControllerWithMockedAuthorization
                (_pictureServiceMock, _accountServiceMock, _fileManagerServiceMock, _webHostEnviromentMock);

            //Act
            var result = await pictureController.RemovePicture(pictureId) as RedirectToActionResult;

            //Assert
            Assert.Equal("Error", result.ActionName);

            _pictureServiceMock.Verify(x => x.RemovePicture(pictureId), Times.Once());
        }

        [Fact]
        public void AddPicture_GET_ExpectedRegistrationPageAsResult()
        {
            //Assert
            var pictureController = new PictureController(_pictureServiceMock.Object, _accountServiceMock.Object,
                _fileManagerServiceMock.Object, _webHostEnviromentMock.Object);

            //Act
            var result = pictureController.AddPicture();

            //Assert
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public async Task AddPicture_POST_ExpectedRedirectToMyPicturesPage_WhenValidModelAndResponsesIsSuccess()
        {
            //Assert
            var model = TestEntitiesProvider.GetPictureViewModel();
            var account = TestEntitiesProvider.GetAccount();
            var fileManagerServiceResponse = new Response<bool>
            {
                Data = true,
                StatusCode = StatusCode.Success
            };

            var pictureServiceResponse = new Response<PictureViewModel>
            {
                StatusCode = StatusCode.Success
            };

            _accountServiceMock.Setup(x => x.GetAccountByLogin(account.Login)).ReturnsAsync(account);
            _pictureServiceMock.Setup(x => x.AddPicture(model)).ReturnsAsync(pictureServiceResponse);
            _fileManagerServiceMock.Setup(x => x.GetUniqueFileName(It.IsAny<string>())).Returns("uniqueFileName");
            _fileManagerServiceMock.Setup(x => x.GetSavePath(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns("savePath");
            _fileManagerServiceMock.Setup(x => x.SavePictureFile(It.IsAny<string>(), It.IsAny<PictureViewModel>())).ReturnsAsync(fileManagerServiceResponse);

            var pictureController = TestEntitiesProvider.GetPictureControllerWithMockedAuthorization
                (_pictureServiceMock, _accountServiceMock, _fileManagerServiceMock, _webHostEnviromentMock);

            //Act
            var result = await pictureController.AddPicture(model) as RedirectToActionResult;

            //Assert
            Assert.Equal("Picture", result.ControllerName);
            Assert.Equal("MyPictures", result.ActionName);

            _accountServiceMock.Verify(x => x.GetAccountByLogin(account.Login), Times.Once());
            _pictureServiceMock.Verify(x => x.AddPicture(model), Times.Once());
            _fileManagerServiceMock.Verify(x => x.GetUniqueFileName(It.IsAny<string>()), Times.Once());
            _fileManagerServiceMock.Verify(x => x.GetSavePath(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            _fileManagerServiceMock.Verify(x => x.SavePictureFile(It.IsAny<string>(), It.IsAny<PictureViewModel>()), Times.Once());
        }

        [Fact]
        public async Task AddPicture_POST_ExpectedReturnToView_WhenFileManagerServiceResponseIsBad()
        {
            //Assert
            var model = TestEntitiesProvider.GetPictureViewModel();
            var account = TestEntitiesProvider.GetAccount();
            var fileManagerServiceResponse = new Response<bool>
            {
                Description = "Error description",
                StatusCode = StatusCode.ServerError
            };

            var pictureServiceResponse = new Response<PictureViewModel>
            {
                StatusCode = StatusCode.Success
            };

            _accountServiceMock.Setup(x => x.GetAccountByLogin(account.Login)).ReturnsAsync(account);
            _fileManagerServiceMock.Setup(x => x.GetUniqueFileName(It.IsAny<string>())).Returns("uniqueFileName");
            _fileManagerServiceMock.Setup(x => x.GetSavePath(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns("savePath");
            _fileManagerServiceMock.Setup(x => x.SavePictureFile(It.IsAny<string>(), It.IsAny<PictureViewModel>())).ReturnsAsync(fileManagerServiceResponse);


            var pictureController = TestEntitiesProvider.GetPictureControllerWithMockedAuthorization
                (_pictureServiceMock, _accountServiceMock, _fileManagerServiceMock, _webHostEnviromentMock);

            pictureController.TempData = TestEntitiesProvider.GetMockedTempData(pictureController);

            //Act
            var result = await pictureController.AddPicture(model) as ViewResult;

            //Assert
            Assert.IsType<PictureViewModel>(result.Model);
            Assert.NotEqual(model, result.Model);

            _accountServiceMock.Verify(x => x.GetAccountByLogin(account.Login), Times.Once());
            _fileManagerServiceMock.Verify(x => x.GetUniqueFileName(It.IsAny<string>()), Times.Once());
            _fileManagerServiceMock.Verify(x => x.GetSavePath(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            _fileManagerServiceMock.Verify(x => x.SavePictureFile(It.IsAny<string>(), It.IsAny<PictureViewModel>()), Times.Once());
        }

        [Fact]
        public async Task AddPicture_POST_ExpectedRedirectToMyPicturesPage_WhenPictureServiceResponseIsBad()
        {
            //Assert
            var model = TestEntitiesProvider.GetPictureViewModel();
            var account = TestEntitiesProvider.GetAccount();
            var fileManagerServiceResponse = new Response<bool>
            {
                StatusCode = StatusCode.Success
            };

            var pictureServiceResponse = new Response<PictureViewModel>
            {
                Description = "Error description",
                StatusCode = StatusCode.ServerError
            };

            _accountServiceMock.Setup(x => x.GetAccountByLogin(account.Login)).ReturnsAsync(account);
            _pictureServiceMock.Setup(x => x.AddPicture(model)).ReturnsAsync(pictureServiceResponse);
            _fileManagerServiceMock.Setup(x => x.GetUniqueFileName(It.IsAny<string>())).Returns("uniqueFileName");
            _fileManagerServiceMock.Setup(x => x.GetSavePath(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns("savePath");
            _fileManagerServiceMock.Setup(x => x.SavePictureFile(It.IsAny<string>(), It.IsAny<PictureViewModel>())).ReturnsAsync(fileManagerServiceResponse);


            var pictureController = TestEntitiesProvider.GetPictureControllerWithMockedAuthorization
                (_pictureServiceMock, _accountServiceMock, _fileManagerServiceMock, _webHostEnviromentMock);

            pictureController.TempData = TestEntitiesProvider.GetMockedTempData(pictureController);

            //Act
            var result = await pictureController.AddPicture(model) as ViewResult;

            //Assert
            Assert.IsType<PictureViewModel>(result.Model);
            Assert.NotEqual(model, result.Model);

            _accountServiceMock.Verify(x => x.GetAccountByLogin(account.Login), Times.Once());
            _pictureServiceMock.Verify(x => x.AddPicture(model), Times.Once());
            _fileManagerServiceMock.Verify(x => x.GetUniqueFileName(It.IsAny<string>()), Times.Once());
            _fileManagerServiceMock.Verify(x => x.GetSavePath(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            _fileManagerServiceMock.Verify(x => x.SavePictureFile(It.IsAny<string>(), It.IsAny<PictureViewModel>()), Times.Once());
        }

        [Fact]
        public async Task AddPicture_POST_ExpectedReturnToView_WhenInvalidModel()
        {
            //Assert
            var model = TestEntitiesProvider.GetPictureViewModel();

            var pictureController = new PictureController(_pictureServiceMock.Object, _accountServiceMock.Object,
                _fileManagerServiceMock.Object, _webHostEnviromentMock.Object);
            pictureController.ModelState.AddModelError("", "Invalid Model");

            //Act
            var result = await pictureController.AddPicture(model) as ViewResult;

            //Assert
            Assert.IsType<PictureViewModel>(result.Model);
            Assert.NotEqual(model, result.Model);
        }
    }
}
