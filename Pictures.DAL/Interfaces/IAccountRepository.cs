
using Pictures.Domain.Entities;
using Pictures.Domain.Responses;

namespace Pictures.DAL.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        public Task<Account> GetById(int id);
        public Task<Account> GetByLogin(string login);
        public Task<Account> GetByEmail(string email);
    }
}
