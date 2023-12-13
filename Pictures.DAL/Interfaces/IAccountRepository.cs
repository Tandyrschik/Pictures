using Microsoft.EntityFrameworkCore;
using Pictures.Domain.Entities;

namespace Pictures.DAL.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        public Task<Account> GetByLogin(string login);
        public Task<Account> GetByEmail(string email);
    }
}
