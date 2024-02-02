
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pictures.DAL;
using Pictures.Domain.Entities;
using Pictures.Domain.Enums;
using System.Net;
using Xunit;

namespace Pictures.IntegrationTests.Controllers
{
    public class AccountControllerTests
    {
        //[Fact]
        //public async Task Registration_ExpectedOK_WhenValidGetRequestToRegiastrationPage()
        //{
        //    // Arrange
        //    var webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(_ => { });

        //    var httpClient = webHost.CreateClient();

        //    // Act

        //    var response = await httpClient.GetAsync("Account/Registration");

        //    //Assert
        //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //}

        //[Fact]
        //public async Task Test2()
        //{
        //    //Arrange
        //    var webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        //    {
        //        builder.ConfigureServices(services =>
        //        {
        //            var dbContextDescriptor = services.SingleOrDefault(sd => sd.ServiceType ==
        //                typeof(DbContextOptions<PicturesDbContext>));

        //            services.Remove(dbContextDescriptor);

        //            services.AddDbContext<PicturesDbContext>(options =>
        //            {
        //                options.UseInMemoryDatabase("PicturesTestsDB");
        //            });
        //        });
        //    });

        //    var httpClient = webHost.CreateClient();

        //    var context = webHost.Services.CreateScope().ServiceProvider.GetService<PicturesDbContext>();

        //    List<Account> accounts = new()
        //    {
        //        new()
        //        {
        //            Id = 0,
        //            Login = "TestLogin0",
        //            Password = "TestPassword0",
        //            Email = "TestEmail0",
        //            Name = "TestName0",
        //            Surname = "TestSurname0",
        //            Role = Role.DefаultUser
        //        },
        //        new()
        //        {
        //            Id = 1,
        //            Login = "TestLogin1",
        //            Password = "TestPassword1",
        //            Email = "TestEmail1",
        //            Name = "TestName1",
        //            Surname = "TestSurname1",
        //            Role = Role.DefаultUser
        //        }
        //    };

        //    List<Picture> pictures = new()
        //    {
        //        new()
        //        {
        //            Id = 0,
        //            Address = "TestAddress0",
        //            Name = "TestName0",
        //            AccountId = 0
        //        },
        //        new()
        //        {
        //            Id = 1,
        //            Address = "TestAddress1",
        //            Name = "TestName1",
        //            AccountId = 0
        //        },
        //        new()
        //        {
        //            Id = 2,
        //            Address = "TestAddress2",
        //            Name = "TestName2",
        //            AccountId = 1
        //        },
        //        new()
        //        {
        //            Id = 3,
        //            Address = "TestAddress3",
        //            Name = "TestName3",
        //            AccountId = 1
        //        },
        //    };

        //    await context.Account.AddRangeAsync(accounts);
        //    await context.Picture.AddRangeAsync(pictures);
        //    await context.SaveChangesAsync();

        //    var content = new StringContent()

        //    // Act


        //}

        //[Fact]
        //public void Login() 
        //{

        //}

        //[Fact]
        //public void Logout()
        //{

        //}

        //public RegistrationViewModel GetRegistarionViewModel()
        //{
        //    return new RegistrationViewModel()
        //    {
        //        Login = "TestLogin",
        //        Password = "TestPassword",
        //        PasswordConfirm = "TestPassword",
        //        Email = "Test@Mail.com",
        //        Name = "TestName",
        //        Surname = "TestSurname"
        //    };
        //}

        //public List<Claim> GetClaims(RegistrationViewModel model)
        //{
        //    return new List<Claim>
        //    {
        //        new Claim(ClaimsIdentity.DefaultNameClaimType, model.Login),
        //        new Claim(ClaimsIdentity.DefaultRoleClaimType, Role.DefаultUser.ToString())
        //    };
        //}
    }
}
