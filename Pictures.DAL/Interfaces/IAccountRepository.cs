using Microsoft.EntityFrameworkCore;
using Pictures.Domain.Entities;

namespace Pictures.DAL.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        public Account GetByLogin(string login);
        public Account GetByEmail(string email);
    }
}
