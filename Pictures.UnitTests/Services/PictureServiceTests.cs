
using Moq;
using Pictures.Controllers;
using Pictures.DAL.Interfaces;
using Pictures.Domain.Entities;
using Pictures.Domain.Enums;
using Pictures.Services.Classes;
using Xunit;

namespace Pictures.UnitTests.Services
{
    public class PictureServiceTests
    {
        //Setup
        private readonly Mock<IPictureRepository> _pictureRepositoryMock;

        public PictureServiceTests()
        {
            _pictureRepositoryMock = new Mock<IPictureRepository>();
        }
        //Setup

        [Fact]
        public async Task AddPicture_ExpectedSuccess_WhenValidPictureModel()
        {
            //Arrange
            var model = TestEntitiesProvider.GetPictureViewModel();
            var picture = TestEntitiesProvider.GetPicture(model);

            _pictureRepositoryMock.Setup(x => x.Add(picture)).ReturnsAsync(true);
            var pictureService = new PictureService(_pictureRepositoryMock.Object);

            //Act
            var actualResponse = await pictureService.AddPicture(model);

            //Assert
            Assert.Equal(StatusCode.Success, actualResponse.StatusCode);
        }

        [Fact]
        public async Task RemovePicture_ExpectedSuccess_WhenPictureWasDeletedFromDB()
        {
            //Arrange
            var model = TestEntitiesProvider.GetPictureViewModel();
            var picture = TestEntitiesProvider.GetPicture(model);

            _pictureRepositoryMock.Setup(x => x.GetById(picture.Id)).ReturnsAsync(picture);
            _pictureRepositoryMock.Setup(x => x.Remove(picture)).ReturnsAsync(true);
            var pictureService = new PictureService(_pictureRepositoryMock.Object);

            //Act
            var actualResponse = await pictureService.RemovePicture(picture.Id);

            //Assert
            Assert.Equal(StatusCode.Success, actualResponse.StatusCode);
            _pictureRepositoryMock.Verify(x => x.GetById(picture.Id), Times.Once());
            _pictureRepositoryMock.Verify(x => x.Remove(picture), Times.Once());
        }

        [Fact]
        public async Task RemovePicture_ExpectedNotFound_WhenPictureNotFoundInDB()
        {
            //Arrange
            var model = TestEntitiesProvider.GetPictureViewModel();
            Picture picture = null;

            _pictureRepositoryMock.Setup(x => x.GetById(0)).ReturnsAsync(picture);
            var pictureService = new PictureService(_pictureRepositoryMock.Object);

            //Act
            var actualResponse = await pictureService.RemovePicture(0);

            //Assert
            Assert.Equal(StatusCode.NotFound, actualResponse.StatusCode);
            _pictureRepositoryMock.Verify(x => x.GetById(0), Times.Once());
        }

        [Fact]
        public async Task GetPicture_ExpectedSuccess_WhenPictureWasFoundInDB()
        {
            //Arrange
            var model = TestEntitiesProvider.GetPictureViewModel();
            var picture = TestEntitiesProvider.GetPicture(model);

            _pictureRepositoryMock.Setup(x => x.GetById(picture.Id)).ReturnsAsync(picture);
            var pictureService = new PictureService(_pictureRepositoryMock.Object);

            //Act
            var actualResponse = await pictureService.GetPicture(picture.Id);

            //Assert
            Assert.Equal(StatusCode.Success, actualResponse.StatusCode);
            _pictureRepositoryMock.Verify(x => x.GetById(picture.Id), Times.Once());
        }

        [Fact]
        public async Task GetPicture_ExpectedNotFound_WhenPictureNotFoundInDB()
        {
            //Arrange
            Picture picture = null;

            _pictureRepositoryMock.Setup(x => x.GetById(0)).ReturnsAsync(picture);
            var pictureService = new PictureService(_pictureRepositoryMock.Object);

            //Act
            var actualResponse = await pictureService.GetPicture(0);

            //Assert
            Assert.Equal(StatusCode.NotFound, actualResponse.StatusCode);
            _pictureRepositoryMock.Verify(x => x.GetById(0), Times.Once());
        }

        [Fact]
        public async Task GetPictures_ExpectedSuccess_WhenPicturesWasFoundInDB()
        {
            //Arrange

            var picturesList = TestEntitiesProvider.GetPicturesList();

            _pictureRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(picturesList);
            var pictureService = new PictureService(_pictureRepositoryMock.Object);

            //Act
            var actualResponse = await pictureService.GetAllPictures();

            //Assert
            Assert.Equal(StatusCode.Success, actualResponse.StatusCode);
            Assert.True(actualResponse.Data.Count() == picturesList.Count);
            _pictureRepositoryMock.Verify(x => x.GetAll(), Times.Once());
        }

        [Fact]
        public async Task GetPictures_ExpectedNotFound_WhenPicturesNotFoundInDB()
        {
            //Arrange
            var picturesListWithoutObjectsWithin = new List<Picture>();

            _pictureRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(picturesListWithoutObjectsWithin);
            var pictureService = new PictureService(_pictureRepositoryMock.Object);

            //Act
            var actualResponse = await pictureService.GetAllPictures();

            //Assert
            Assert.Equal(StatusCode.NotFound, actualResponse.StatusCode);
            _pictureRepositoryMock.Verify(x => x.GetAll(), Times.Once());
        }

        [Fact]
        public async Task GetPicturesOfAccount_ExpectedSuccess_WhenPicturesWasFoundInDB()
        {
            //Arrange
            var picturesListOfOneAccount = TestEntitiesProvider.GetPicturesListOfOneAccount();
            int accountId = 0;

            _pictureRepositoryMock.Setup(x => x.GetAll(accountId)).ReturnsAsync(picturesListOfOneAccount);
            var pictureService = new PictureService(_pictureRepositoryMock.Object);

            //Act
            var actualResponse = await pictureService.GetPicturesOfAccount(accountId);

            //Assert
            Assert.Equal(StatusCode.Success, actualResponse.StatusCode);
            Assert.True(actualResponse.Data.All(x => x.AccountId == accountId));
            _pictureRepositoryMock.Verify(x => x.GetAll(accountId), Times.Once());
        }

        [Fact]
        public async Task GetPicturesOfAccount_ExpectedNotFound_WhenPicturesNotFoundInDB()
        {
            //Arrange
            var picturesListWithoutObjectsWithin = new List<Picture>();
            int accountId = 0;

            _pictureRepositoryMock.Setup(x => x.GetAll(accountId)).ReturnsAsync(picturesListWithoutObjectsWithin);
            var pictureService = new PictureService(_pictureRepositoryMock.Object);

            //Act
            var actualResponse = await pictureService.GetPicturesOfAccount(accountId);

            //Assert
            Assert.Equal(StatusCode.NotFound, actualResponse.StatusCode);
            _pictureRepositoryMock.Verify(x => x.GetAll(accountId), Times.Once());
        }
    }
}