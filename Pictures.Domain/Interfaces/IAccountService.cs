
using Pictures.Domain.Entities;
using Pictures.Domain.Interfaces;
using Pictures.Domain.ViewModels.Account;
using Pictures.Domain.ViewModels.Picture;
using System.Security.Claims;

namespace Pictures.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IResponse<ClaimsIdentity>> Register(RegistrationViewModel model);
        Task<IResponse<ClaimsIdentity>> Login(LoginViewModel model);
        Task<Account> GetAccountByLogin(string login);
    }
}
