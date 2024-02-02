
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Pictures.Controllers;
using Pictures.Domain.Entities;
using Pictures.Domain.Enums;
using Pictures.Domain.Responses;
using Pictures.Services.Interfaces;
using Xunit;

namespace Pictures.UnitTests.Controllers
{
    public class HomeControllerTests
    {
        //Setup
        private readonly Mock<IPictureService> _pictureServiceMock;

        public HomeControllerTests()
        {
            _pictureServiceMock = new Mock<IPictureService>();
        }
        //Setup

        [Fact]
        public async Task Index_GET_ExpectedListOfAllPictures_WhenServerResponseIsSuccess()
        {
            //Assert
            var pictureServiceResponse = new Response<IEnumerable<Picture>>
            {
                Data = TestEntitiesProvider.GetPicturesList(),
                StatusCode = StatusCode.Success
            };

            var loggerMock = new Mock<ILogger<PictureController>>();
            _pictureServiceMock.Setup(x => x.GetAllPictures()).ReturnsAsync(pictureServiceResponse);

            var homeController = new HomeController(loggerMock.Object, _pictureServiceMock.Object);

            //Act
            var result = await homeController.Index() as ViewResult;

            //Assert
            Assert.IsType<List<Picture>>(result.Model);
            Assert.Equal(pictureServiceResponse.Data, result.Model);
            _pictureServiceMock.Verify(x => x.GetAllPictures(), Times.Once());   
        }

        [Fact]
        public async Task Index_GET_ExpectedRedirectToErrorPage_WhenServerResponseIsBad()
        {
            //Assert
            var pictureServiceResponse = new Response<IEnumerable<Picture>>
            {
                StatusCode = StatusCode.NotFound
            };

            var loggerMock = new Mock<ILogger<PictureController>>();
            _pictureServiceMock.Setup(x => x.GetAllPictures()).ReturnsAsync(pictureServiceResponse);

            var homeController = new HomeController(loggerMock.Object, _pictureServiceMock.Object);

            //Act
            var result = await homeController.Index() as RedirectToActionResult;

            //Assert
            Assert.Equal("Error", result.ActionName);
        }
    }
}
