using Pictures.DAL.Interfaces;
using Pictures.Domain.Entities;
using Pictures.Domain.Enums;
using Pictures.Domain.Interfaces;
using Pictures.Domain.Responses;
using Pictures.Domain.ViewModels.Account;
using Pictures.Services.Interfaces;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Pictures.Services.Classes
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<IResponse<ClaimsIdentity>> Register(RegistrationViewModel model)
        {
            try
            {
                var login = await _accountRepository.GetByLogin(model.Login);
                if (login is not null)
                {
                    return new Response<ClaimsIdentity>()
                    {
                        Description = "An account with the specified login already exists",
                        StatusCode = StatusCode.SpecifiedDataExist
                    };
                }

                var email = await _accountRepository.GetByEmail(model.Email);
                if (email is not null)
                {
                    return new Response<ClaimsIdentity>()
                    {
                        Description = "An account with the specified email already exists",
                        StatusCode = StatusCode.SpecifiedDataExist
                    };
                }

                var account = new Account
                {
                    Login = model.Login,
                    Password = Encrypt(model.Password),
                    Email = model.Email,
                    Name = model.Name,
                    Surname = model.Surname,
                    Role = Role.DefаultUser
                };

                await _accountRepository.Add(account);

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

        public async Task<IResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            {
                var account = await _accountRepository.GetByLogin(model.Login);
                if (account is null)
                {
                    return new Response<ClaimsIdentity>()
                    {
                        Description = "Account not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                if (account.Password != Encrypt(model.Password))
                {
                    return new Response<ClaimsIdentity>()
                    {
                        Description = "Incorrect password",
                        StatusCode = StatusCode.IncorrectData
                    };
                }

                var claims = Authenticate(account);

                return new Response<ClaimsIdentity>()
                {
                    Data = claims,
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

        public async Task<Account> GetAccountByLogin(string login)
        {
            return await _accountRepository.GetByLogin(login);
        }

        private static ClaimsIdentity Authenticate(Account account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, account.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, account.Role.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

        public static string Encrypt(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                return hash;
            }
        }
    }
}
