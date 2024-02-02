
using Microsoft.EntityFrameworkCore;
using Pictures.DAL;
using Pictures.DAL.Interfaces;
using Pictures.DAL.Repositories;
using Pictures.Domain.Entities;
using Xunit;

namespace Pictures.UnitTests.Repository
{
    public class AccountRepositoryTests 
    {
        //Setup
        private readonly PicturesDbContext _picturesDbContext;
        private readonly IAccountRepository _accountRepository;

        public AccountRepositoryTests() 
        {
            _picturesDbContext = GetInMemoryContext();
            _accountRepository = new AccountRepository(_picturesDbContext);
        }

        private PicturesDbContext GetInMemoryContext()
        {
            var builder = new DbContextOptionsBuilder<PicturesDbContext>();
            builder.UseInMemoryDatabase("TestDatabase");

            return new PicturesDbContext(builder.Options);
        }
        //Setup

        [Fact]
        public async Task Add_ExpectedTrue_WhenAccountHasBeenAddedToDB()
        {
            //Arrange
            var expectedAccount = TestEntitiesProvider.GetAccount();

            //Act
            var addRepositoryResponse = await _accountRepository.Add(expectedAccount);

            //Assert
            var actualAccount = await _accountRepository.GetById(expectedAccount.Id);
            Assert.True(addRepositoryResponse);
            Assert.Equal(expectedAccount, actualAccount);

            await _accountRepository.Remove(actualAccount);
        }

        [Fact]
        public async Task Remove_ExpectedTrue_WhenAccountWasRemovedFromDB()
        {
            //Arrange
            var account = TestEntitiesProvider.GetAccount();
            var addRepositoryResponse = await _accountRepository.Add(account);

            //Act
            var actualBool = await _accountRepository.Remove(account);

            //Assert
            var actualAccount = await _accountRepository.GetById(account.Id);
            Assert.True(addRepositoryResponse);
            Assert.True(actualBool);
            Assert.True(actualAccount is null);
        }

        [Fact]
        public async Task GetById_ExpectedAccountEntityWithSpecifiedIdFromDB()
        {
            //Arrange
            var expectedAccount = TestEntitiesProvider.GetAccount();
            var addRepositoryResponse = await _accountRepository.Add(expectedAccount);

            //Act
            var actualAccount = await _accountRepository.GetById(expectedAccount.Id);

            //Assert
            Assert.True(addRepositoryResponse);
            Assert.Equal(expectedAccount, actualAccount);

            await _accountRepository.Remove(actualAccount);
        }

        [Fact]
        public async Task GetByLogin_ExpectedAccountEntityWithSpecifiedLoginFromDB()
        {
            //Arrange
            var expectedAccount = TestEntitiesProvider.GetAccount();
            var addRepositoryResponse = await _accountRepository.Add(expectedAccount);

            //Act
            var actualAccount = await _accountRepository.GetByLogin(expectedAccount.Login);

            //Assert
            Assert.True(addRepositoryResponse);
            Assert.Equal(expectedAccount, actualAccount);

            await _accountRepository.Remove(actualAccount);
        }

        [Fact]
        public async Task GetByEmail_ExpectedAccountEntityWithSpecifiedEmailFromDB()
        {
            //Arrange
            var expectedAccount = TestEntitiesProvider.GetAccount();
            var addRepositoryResponse = await _accountRepository.Add(expectedAccount);

            //Act
            var actualAccount = await _accountRepository.GetByEmail(expectedAccount.Email);

            //Assert
            Assert.True(addRepositoryResponse);
            Assert.Equal(expectedAccount, actualAccount);

            await _accountRepository.Remove(actualAccount);
        }

        [Fact]
        public async Task GetAll_ExpectedListOfAllAccountEntitiesFromDB()
        {
            //Arrange
            var expectedAccountsList = TestEntitiesProvider.GetListOfAccounts();
            foreach (var account in expectedAccountsList)
            {
                await _accountRepository.Add(account);
            }

            //Act
            var getAllRepositoryResponse = await _accountRepository.GetAll();

            //Assert
            Assert.Equal(expectedAccountsList, getAllRepositoryResponse);

            foreach(var account in expectedAccountsList)
            {
                await _accountRepository.Remove(account);
            }
        }
    }
}
