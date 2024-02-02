using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Pictures.Controllers;
using Pictures.Domain.Entities;
using Pictures.Domain.Enums;
using Pictures.Domain.ViewModels.Account;
using Pictures.Domain.ViewModels.Picture;
using Pictures.Services.Classes;
using System.Security.Claims;
using Pictures.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace Pictures.UnitTests
{
    public class TestEntitiesProvider
    {
        //Account
        public static RegistrationViewModel GetRegistrationViewModel()
        {
            return new RegistrationViewModel()
            {
                Login = "TestLogin0",
                Password = "TestPassword0",
                PasswordConfirm = "TestPassword0",
                Email = "Test@mail.com0",
                Name = "TestName0",
                Surname = "TestSurname0"
            };
        }

        public static RegistrationViewModel GetInvalidRegistrationViewModel()
        {
            return new RegistrationViewModel();
        }

        public static LoginViewModel GetLoginViewModel()
        {
            return new LoginViewModel()
            {
                Login = "TestLogin0",
                Password = "TestPassword0"
            };
        }
        public static LoginViewModel GetInvalidLoginViewModel()
        {
            return new LoginViewModel();
        }

        public static LoginViewModel GetLoginViewModelWithWrongPassword()
        {
            return new LoginViewModel()
            {
                Login = "TestLogin0",
                Password = "WrongPassword"
            };
        }

        public static Account GetAccount()
        {
            return new Account()
            {
                Id = 0,
                Login = "TestLogin0",
                Password = AccountService.Encrypt("TestPassword0"),
                Email = "Test@Mail.com0",
                Name = "TestName0",
                Surname = "TestSurname0",
                Role = Role.DefаultUser
            };
        }

        public static List<Account> GetListOfAccounts()
        {
            return new List<Account>()
            {
                new()
                {
                    Login = "TestLogin0",
                    Password = AccountService.Encrypt("TestPassword0"),
                    Email = "Test@Mail.com0",
                    Name = "TestName0",
                    Surname = "TestSurname0",
                    Role = Role.DefаultUser
                },
                new()
                {
                    Login = "TestLogin1",
                    Password = AccountService.Encrypt("TestPassword1"),
                    Email = "Test@Mail.com1",
                    Name = "TestName1",
                    Surname = "TestSurname1",
                    Role = Role.DefаultUser
                },
                new()
                {
                    Login = "TestLogin2",
                    Password = AccountService.Encrypt("TestPassword2"),
                    Email = "Test@Mail.com2",
                    Name = "TestName2",
                    Surname = "TestSurname2",
                    Role = Role.DefаultUser
                },
                new()
                {
                    Login = "TestLogin3",
                    Password = AccountService.Encrypt("TestPassword3"),
                    Email = "Test@Mail.com3",
                    Name = "TestName3",
                    Surname = "TestSurname3",
                    Role = Role.DefаultUser
                }
            };
        }

        public static ClaimsIdentity GetClaims(Account account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, account.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, account.Role.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

        public static AccountController GetAccountControllerWithMockedAuthorization(Mock<IAccountService> accountServiceMock)
        {
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock
                .Setup(_ => _.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
                .Returns(Task.FromResult((object)null));

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(_ => _.GetService(typeof(IAuthenticationService)))
                .Returns(authServiceMock.Object);

            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            serviceProviderMock.Setup(_ => _.GetService(typeof(IUrlHelperFactory)))
                .Returns(urlHelperFactory.Object);

            var controller = new AccountController(accountServiceMock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        // How mock RequestServices?
                        RequestServices = serviceProviderMock.Object
                    }
                }
            };

            return controller;
        }

        //Picture
        public static Picture GetPicture(PictureViewModel model)
        {
            return new Picture
            {
                Address = model.Address,
                Name = model.Name,
                AccountId = model.AccountId
            };
        }

        public static PictureViewModel GetPictureViewModel()
        {
            var content = "Hello World from a Fake File";
            var fileName = "test.jpg";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

            return new PictureViewModel
            {
                Name = "TestName",
                Address = "TestAddress",
                AccountId = 0,
                ImageFile = file
            };
        }

        public static List<Picture> GetPicturesList()
        {
            return new List<Picture>
            {
                new (){Name = "TestName0",Address = "TestAddress0",AccountId = 0},
                new (){Name = "TestName1",Address = "TestAddress1",AccountId = 1},
                new (){Name = "TestName2",Address = "TestAddress2",AccountId = 2},
                new (){Name = "TestName3",Address = "TestAddress3",AccountId = 3}
            };
        }

        public static List<Picture> GetPicturesListOfOneAccount()
        {
            return new List<Picture>
            {
                new (){Id = 0,Name = "TestName0",Address = "TestAddress0",AccountId = 0},
                new (){Id = 1,Name = "TestName1",Address = "TestAddress1",AccountId = 0},
                new (){Id = 2,Name = "TestName2",Address = "TestAddress2",AccountId = 0},
                new (){Id = 3,Name = "TestName3",Address = "TestAddress3",AccountId = 0}
            };
        }

        public static PictureController GetPictureControllerWithMockedAuthorization(
            Mock<IPictureService> pictureServiceMock,
            Mock<IAccountService> accountServiceMock,
            Mock<IFileManagerService> fileManagerService,
            Mock<IWebHostEnvironment> webHostEnviromentMock)
        {
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock
                .Setup(_ => _.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
                .Returns(Task.FromResult((object)null));

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(_ => _.GetService(typeof(IAuthenticationService)))
                .Returns(authServiceMock.Object);

            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            serviceProviderMock.Setup(_ => _.GetService(typeof(IUrlHelperFactory)))
                .Returns(urlHelperFactory.Object);

            var controller = new PictureController(pictureServiceMock.Object, accountServiceMock.Object,
                                                   fileManagerService.Object, webHostEnviromentMock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        RequestServices = serviceProviderMock.Object,
                        User = new ClaimsPrincipal(GetClaims(GetAccount()))
                    }
                }
            };

            return controller;
        }


        //Other
        public static TempDataDictionary GetMockedTempData(PictureController pictureController)
        {
            var tempDataProviderMock = new Mock<ITempDataProvider>();
            var tempdata = new TempDataDictionary(pictureController.HttpContext, tempDataProviderMock.Object)
            {
                ["returnurl"] = ""
            };

            return tempdata;
        }

        public static Mock<IWebHostEnvironment> GetMockedWebHostEnvironmentWithMockedWebRootPath()
        {
            var webHostEnviromentMock = new Mock<IWebHostEnvironment>();
            webHostEnviromentMock.Setup(x => x.WebRootPath).Returns("Test_Root_Path");

            return webHostEnviromentMock;
        }
    }
}
