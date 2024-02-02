
using Moq;
using Xunit;
using Pictures.DAL.Interfaces;
using Pictures.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Pictures.DAL.Repositories;
using Pictures.DAL;

namespace Pictures.UnitTests.Repository
{
    public class PictureRepositoryTests
    {
        //Setup
        private readonly PicturesDbContext _picturesDbContext;
        private readonly IPictureRepository _pictureRepository;

        public PictureRepositoryTests()
        {
            _picturesDbContext = GetInMemoryContext();
            _pictureRepository = new PictureRepository(_picturesDbContext);
        }

        private PicturesDbContext GetInMemoryContext()
        {
            var builder = new DbContextOptionsBuilder<PicturesDbContext>();
            builder.UseInMemoryDatabase("TestDatabase");

            return new PicturesDbContext(builder.Options);
        }
        //Setup

        [Fact]
        public async Task Add_ExpectedTrue_WhenPictureHasBeenAddedToDB()
        {
            //Arrange
            var model = TestEntitiesProvider.GetPictureViewModel();
            var expectedPicture = TestEntitiesProvider.GetPicture(model);

            //Act
            var resultBool = await _pictureRepository.Add(expectedPicture);

            //Assert
            var actualPicture = await _pictureRepository.GetById(expectedPicture.Id);
            Assert.True(resultBool);
            Assert.Equal(expectedPicture, actualPicture);

            await _pictureRepository.Remove(actualPicture);
        }

        [Fact]
        public async Task Remove_ExpectedTrue_WhenPictureWasDeletedFromDB()
        {
            //Arrange
            var model = TestEntitiesProvider.GetPictureViewModel();
            var expectedPicture = TestEntitiesProvider.GetPicture(model);
            var addingResult = await _pictureRepository.Add(expectedPicture);

            //Act
            var actualBool = await _pictureRepository.Remove(expectedPicture);

            //Assert
            var actualPicture = await _pictureRepository.GetById(expectedPicture.Id);
            Assert.True(addingResult);
            Assert.True(actualBool);
            Assert.True(actualPicture is null);
        }

        [Fact]
        public async Task GetById_ExpectedOnePictureEntityFromDB()
        {
            //Arrange
            var model = TestEntitiesProvider.GetPictureViewModel();
            var expectedPicture = TestEntitiesProvider.GetPicture(model);
            var addingResult = await _pictureRepository.Add(expectedPicture);

            //Act
            var actualPicture = await _pictureRepository.GetById(expectedPicture.Id);

            //Assert
            Assert.True(addingResult);
            Assert.Equal(expectedPicture, actualPicture);

            await _pictureRepository.Remove(actualPicture);
        }

        [Fact]
        public async Task GetAll_ExpectedListOfAllPictureEntitiesFromDB()
        {
            //Arrange
            var expectedPicturesList = TestEntitiesProvider.GetPicturesList();

            foreach (var picture in expectedPicturesList)
            {
                await _pictureRepository.Add(picture);
            }

            //Act
            var actualPicturesList = await _pictureRepository.GetAll();

            //Assert
            Assert.Equal(expectedPicturesList, actualPicturesList);

            foreach (var picture in actualPicturesList)
            {
                await _pictureRepository.Remove(picture);
            }
        }

        [Fact]
        public async Task GetAll_ExpectedListAllPictureEntitiesOfOneAccountFromDB()
        {
            //Arrange
            var account = new Account() { Id = 0 };
            var expectedPicturesList = TestEntitiesProvider.GetPicturesList();
            int picturesCountWithSpecifiedAccountId = expectedPicturesList.Count(x => x.AccountId == account.Id);

            foreach (var picture in expectedPicturesList)
            {
                await _pictureRepository.Add(picture);
            }

            //Act
            var actualPicturesList = await _pictureRepository.GetAll(account.Id);

            //Assert
            Assert.Equal(picturesCountWithSpecifiedAccountId, actualPicturesList.Count());
            Assert.NotEqual(expectedPicturesList, actualPicturesList);

            foreach (var picture in expectedPicturesList)
            {
                await _pictureRepository.Remove(picture);
            }
        }
    }
}
