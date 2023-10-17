using Pictures.DAL.Interfaces;
using Pictures.Domain.Entities;
using Pictures.Domain.Enums;
using Pictures.Domain.Helpers;
using Pictures.Domain.Interfaces;
using Pictures.Domain.Responses;
using Pictures.Domain.ViewModels.Account;
using Pictures.Services.Interfaces;
using System.Security.Claims;

namespace Pictures.Services.Classes
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository) =>
            (_accountRepository) = (accountRepository);

        public IResponse<ClaimsIdentity> Register(RegistrationViewModel registrationViewModel)
        {
            try
            {
                var accountIsExist = _accountRepository.GetByLogin(registrationViewModel.Login);
                if (accountIsExist is not null)
                {
                    return new Response<ClaimsIdentity>()
                    {
                        Description = "An account with the specified login already exists"
                    };
                }

                accountIsExist = _accountRepository.GetByEmail(registrationViewModel.Email);
                if(accountIsExist is not null)
                {
                    return new Response<ClaimsIdentity>()
                    {
                        Description = "An account with the specified email already exists"
                    };
                }

                var account = new Account
                {
                    Login = registrationViewModel.Login,
                    Password = EncrypterHelper.Encrypt(registrationViewModel.Password),
                    Email = registrationViewModel.Email,
                    Name = registrationViewModel.Name,
                    Surname = registrationViewModel.Surname,
                    Role = Role.DefаultUser
                };

                _accountRepository.Add(account);
                var claims = Authenticate(account);

                return new Response<ClaimsIdentity>()
                {
                    Data = claims,
                    Description = "Aaccount successfully registered",
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new Response<ClaimsIdentity>()
                {
                    Description = $"[AddAccount] : {ex.Message}",
                    StatusCode = StatusCode.ServerError
                };
            }
        }

        private ClaimsIdentity Authenticate(Account account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, account.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, account.Role.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
