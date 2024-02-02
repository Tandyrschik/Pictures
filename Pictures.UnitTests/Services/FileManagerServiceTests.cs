
using Microsoft.AspNetCore.Http;
using Moq;
using Pictures.Domain.Enums;
using Pictures.Domain.ViewModels.Picture;
using Pictures.Services.Classes;
using Xunit;

namespace Pictures.UnitTests.Services
{
    public class FileManagerServiceTests
    {
        //Setup
        private readonly Mock<IFormFile> _formFileMock;
        private readonly FileManagerService _fileManagerService;

        public FileManagerServiceTests()
        {
            _formFileMock = new Mock<IFormFile>();
            _fileManagerService = new FileManagerService();
        }
        //Setup

        [Fact]
        public async Task SavePictureFile_ShouldReturnSuccess_WhenFileIsSaved()
        {
            var filePath = "testFilePath";
            var model = new PictureViewModel() 
            { 
                ImageFile = _formFileMock.Object 
            };

            var result = await _fileManagerService.SavePictureFile(filePath, model);

            Assert.True(result.Data);
            Assert.Equal(StatusCode.Success, result.StatusCode);
        }

        [Fact]
        public async Task SavePictureFile_ShouldReturnServerError_ServerError()
        {
            var filePath = "testFilePath";
            var model = new PictureViewModel() 
            { 
                ImageFile = null 
            };

            var result = await _fileManagerService.SavePictureFile(filePath, model);

            Assert.False(result.Data);
            Assert.Equal(StatusCode.ServerError, result.StatusCode);
        }

        [Fact]
        public void GetUniqueFileName_ExpectedNewUniqueString_WithSameFileExtension()
        {
            //Arrange
            string fileName = "12345.jpg";

            //Act
            string actualFileName = _fileManagerService.GetUniqueFileName(fileName);

            //Assert
            Assert.NotEqual(fileName, actualFileName);
            Assert.Equal(Path.GetExtension(fileName), Path.GetExtension(actualFileName));
        }

        [Fact]
        public void GetSavePath_ExpectedNewUniqueString_WithPathToFile()
        {
            //Arrange
            string fileName = "12345.jpg";
            string path = @"C:\Users\Tandy\source\repos\Pictures\Pictures\wwwroot\";
            string folderName = "img";

            string expectedPath = path + folderName + @"\" + fileName;

            //Act
            string actualPath = _fileManagerService.GetSavePath(path, folderName, fileName);

            //Assert
            Assert.Equal(expectedPath, actualPath);
        }
    }
}
