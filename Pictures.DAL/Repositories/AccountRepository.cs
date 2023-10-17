
using Pictures.DAL.Interfaces;
using Pictures.Domain.Entities;

namespace Pictures.DAL.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly PicturesDbContext _context;
        public AccountRepository(PicturesDbContext context) =>
            (_context) = (context);

        public bool Add(Account entity)
        {
            _context.Add(entity);
            _context.SaveChanges();

            return true;
        }

        public bool Remove(Account entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();

            return true;
        }

        public Account GetById(int id)
        {
            return _context.Account.FirstOrDefault(a => a.Id == id);
        }

        public Account GetByLogin(string login)
        {
            return _context.Account.FirstOrDefault(a => a.Login == login);
        }

        public Account GetByEmail(string email)
        {
            return _context.Account.FirstOrDefault(a => a.Email == email);
        }

        public IEnumerable<Account> GetAll()
        {
            return _context.Account.ToList();
        }

    }
}
