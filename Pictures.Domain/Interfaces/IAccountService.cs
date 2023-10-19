
using Pictures.Domain.Interfaces;
using Pictures.Domain.ViewModels.Account;
using Pictures.Domain.ViewModels.Picture;
using System.Security.Claims;

namespace Pictures.Services.Interfaces
{
    public interface IAccountService
    {
        IResponse<ClaimsIdentity> Register(RegistrationViewModel model);
        IResponse<ClaimsIdentity> Login(LoginViewModel model);
    }
}
