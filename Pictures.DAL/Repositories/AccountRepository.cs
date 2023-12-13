
using Microsoft.EntityFrameworkCore;
using Pictures.DAL.Interfaces;
using Pictures.Domain.Entities;

namespace Pictures.DAL.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly PicturesDbContext _context;
        public AccountRepository(PicturesDbContext context) =>
            (_context) = (context);

        public async Task<bool> Add(Account entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Remove(Account entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Account> GetById(int id)
        {
            return await _context.Account.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Account> GetByLogin(string login)
        {
            return await _context.Account.FirstOrDefaultAsync(a => a.Login == login);
        }

        public async Task<Account> GetByEmail(string email)
        {
            return await _context.Account.FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            return await _context.Account.ToListAsync();
        }

    }
}
