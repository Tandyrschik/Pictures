using Microsoft.Identity.Client;
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
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public IResponse<ClaimsIdentity> Register(RegistrationViewModel model)
        {
            try
            {
                var accountIsExist = _accountRepository.GetByLogin(model.Login);
                if (accountIsExist is not null)
                {
                    return new Response<ClaimsIdentity>()
                    {
                        Description = "An account with the specified login already exists"
                    };
                }

                accountIsExist = _accountRepository.GetByEmail(model.Email);
                if (accountIsExist is not null)
                {
                    return new Response<ClaimsIdentity>()
                    {
                        Description = "An account with the specified email already exists"
                    };
                }

                var account = new Account
                {
                    Login = model.Login,
                    Password = EncrypterHelper.Encrypt(model.Password),
                    Email = model.Email,
                    Name = model.Name,
                    Surname = model.Surname,
                    Role = Role.DefаultUser
                };

                _accountRepository.Add(account);
                var claims = Authenticate(account);

                return new Response<ClaimsIdentity>()
                {
                    Data = claims,
                    Description = "Account successfully registered",
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

        public IResponse<ClaimsIdentity> Login(LoginViewModel model)
        {
            try
            {
                var account = _accountRepository.GetByLogin(model.Login);
                if (account is null)
                {
                    return new Response<ClaimsIdentity>()
                    {
                        Description = "Account not found"
                    };
                }

                if (account.Password != EncrypterHelper.Encrypt(model.Password))
                {
                    return new Response<ClaimsIdentity>()
                    {
                        Description = "Incorrect password"
                    };
                }

                var result = Authenticate(account);

                return new Response<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new Response<ClaimsIdentity>()
                {
                    Description = $"[Login] : {ex.Message}",
                    StatusCode = StatusCode.ServerError
                };
            }
        }

        public Account GetAccountByLogin(string login)
        {
            return _accountRepository.GetByLogin(login);
        }

        private ClaimsIdentity Authenticate(Account account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, account.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, account.Role.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
