
using Moq;
using Pictures.DAL.Interfaces;
using Pictures.Domain.Entities;
using Pictures.Domain.Enums;
using Pictures.Services.Classes;
using Xunit;

namespace Pictures.UnitTests.Services
{
    public class AccountServiceTests
    {
        //Setup
        private readonly Mock<IAccountRepository> _accountRepositoryMock;

        public AccountServiceTests()
        {
            _accountRepositoryMock = new Mock<IAccountRepository>();
        }
        //Setup

        [Fact]
        public async Task Register_ExpectedSuccess_WhenValidRegistrationModel()
        {
            // Arrange
            var model = TestEntitiesProvider.GetRegistrationViewModel();
            var account = TestEntitiesProvider.GetAccount();
            Account returnedAccount = null;

            _accountRepositoryMock.Setup(x => x.Add(account)).ReturnsAsync(true);
            _accountRepositoryMock.Setup(x => x.GetByLogin(model.Login)).ReturnsAsync(returnedAccount);
            _accountRepositoryMock.Setup(x => x.GetByEmail(model.Email)).ReturnsAsync(returnedAccount);

            var accountService = new AccountService(_accountRepositoryMock.Object);

            // Act
            var actualResponse = await accountService.Register(model);

            // Assert
            Assert.Equal(StatusCode.Success, actualResponse.StatusCode);
            _accountRepositoryMock.Verify(x => x.GetByLogin(model.Login), Times.Once());
            _accountRepositoryMock.Verify(x => x.GetByEmail(model.Email), Times.Once());
        }

        [Fact]
        public async Task Register_ExpectedSpecifiedDataExist_WhenLoginInModelExistInDB()
        {
            // Arrange
            var model = TestEntitiesProvider.GetRegistrationViewModel();
            var account = TestEntitiesProvider.GetAccount();

            _accountRepositoryMock.Setup(x => x.GetByLogin(model.Login)).ReturnsAsync(account);

            var accountService = new AccountService(_accountRepositoryMock.Object);

            // Act
            var actualResponse = await accountService.Register(model);

            // Assert
            Assert.Equal(StatusCode.SpecifiedDataExist, actualResponse.StatusCode);
            _accountRepositoryMock.Verify(x => x.GetByLogin(model.Login), Times.Once());
        }

        [Fact]
        public async Task Register_ExpectedSpecifiedDataExist_WhenEmailInModelExistInDB()
        {
            // Arrange
            var model = TestEntitiesProvider.GetRegistrationViewModel();
            var account = TestEntitiesProvider.GetAccount();
            Account returnedAccount = null;

            _accountRepositoryMock.Setup(x => x.GetByLogin(model.Login)).ReturnsAsync(returnedAccount);
            _accountRepositoryMock.Setup(x => x.GetByEmail(model.Email)).ReturnsAsync(account);
            var accountService = new AccountService(_accountRepositoryMock.Object);

            // Act
            var actualResponse = await accountService.Register(model);

            // Assert
            Assert.Equal(StatusCode.SpecifiedDataExist, actualResponse.StatusCode);
            _accountRepositoryMock.Verify(x => x.GetByLogin(model.Login), Times.Once());
            _accountRepositoryMock.Verify(x => x.GetByEmail(model.Email), Times.Once());
        }

        [Fact]
        public async Task Login_ExpectedSuccess_WhenValidLoginModel()
        {
            // Arrange
            var model = TestEntitiesProvider.GetLoginViewModel();
            var account = TestEntitiesProvider.GetAccount();

            _accountRepositoryMock.Setup(x => x.GetByLogin(model.Login)).ReturnsAsync(account);
            var accountService = new AccountService(_accountRepositoryMock.Object);

            // Act
            var actualResponse = await accountService.Login(model);

            // Assert
            Assert.Equal(StatusCode.Success, actualResponse.StatusCode);
            _accountRepositoryMock.Verify(x => x.GetByLogin(model.Login), Times.Once());
        }

        [Fact]
        public async Task Login_ExpectedNotFound_WhenAccountWasNotFoundInDB()
        {
            // Arrange
            var model = TestEntitiesProvider.GetLoginViewModel();
            Account returnedAccount = null;

            _accountRepositoryMock.Setup(x => x.GetByLogin(model.Login)).ReturnsAsync(returnedAccount);
            var accountService = new AccountService(_accountRepositoryMock.Object);

            // Act
            var actualResponse = await accountService.Login(model);

            // Assert
            Assert.Equal(StatusCode.NotFound, actualResponse.StatusCode);
            _accountRepositoryMock.Verify(x => x.GetByLogin(model.Login), Times.Once());
        }

        [Fact]
        public async Task Login_ExpectedIncorrectData_WhenIncorrectPassword()
        {
            // Arrange
            var model = TestEntitiesProvider.GetLoginViewModelWithWrongPassword();
            var account = TestEntitiesProvider.GetAccount();

            _accountRepositoryMock.Setup(x => x.GetByLogin(model.Login)).ReturnsAsync(account);
            var accountService = new AccountService(_accountRepositoryMock.Object);

            // Act
            var actualResponse = await accountService.Login(model);

            // Assert
            Assert.Equal(StatusCode.IncorrectData, actualResponse.StatusCode);
            _accountRepositoryMock.Verify(x => x.GetByLogin(model.Login), Times.Once());
        }

        [Fact]
        public void Encrypt_ExpectedThatResultStringAndEnteredPassword_AreNotTheSame()
        {
            //Arrange
            string password = "TestPassword";

            //Act
            string actualPassword = AccountService.Encrypt(password);

            //Assert
            Assert.NotEqual(password, actualPassword);
        }

        [Fact]
        public void Encrypt_ExpectedThatResultStringIsLonger_ThanEnteredPassword()
        {
            //Arrange
            string password = "TestPassword";

            //Act
            string actualPassword = AccountService.Encrypt(password);

            //Assert
            Assert.True(actualPassword.Length > 50);
        }

        [Fact]
        public void Encrypt_ExpectedThreeDifferentStrings_WithThreeDifferentPasswords()
        {
            //Arrange
            string password1 = "TestPassword1";
            string password2 = "TestPassword2";
            string password3 = "TestPassword3";

            //Act
            string actualPassword1 = AccountService.Encrypt(password1);
            string actualPassword2 = AccountService.Encrypt(password2);
            string actualPassword3 = AccountService.Encrypt(password3);

            //Assert
            Assert.True(actualPassword1 != actualPassword2 &&
                        actualPassword1 != actualPassword3 &&
                        actualPassword2 != actualPassword3);
        }

    }
}
