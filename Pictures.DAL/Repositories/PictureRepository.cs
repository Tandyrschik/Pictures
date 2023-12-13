
using Microsoft.EntityFrameworkCore;
using Pictures.DAL.Interfaces;
using Pictures.Domain.Entities;

namespace Pictures.DAL.Repositories
{
	public class PictureRepository : IPictureRepository  // индивидуальный класс-обработчик запросов в БД для объектов Picture
	{                                                    // обращается в БД посредством EntityFramework
		private readonly PicturesDbContext _context;
		public PictureRepository(PicturesDbContext context) => 
			(_context) = (context);

		public async Task<bool> Add(Picture picture) 
		{
			await _context.AddAsync(picture);
			await _context.SaveChangesAsync();

			return true;
		}

		public async Task<bool> Remove(Picture picture)
		{
			_context.Remove(picture);
			await _context.SaveChangesAsync();

			return true;
		}
		public async Task<Picture> GetById(int id)
		{
			return await _context.Picture.FirstOrDefaultAsync(p => p.Id == id);
		}
		public async Task<IEnumerable<Picture>> GetAll()
		{
			return await _context.Picture.ToListAsync();
		}
        public async Task<IEnumerable<Picture>> GetAllOfAccount(int accountId)
        {
            return await _context.Picture.Where(x => x.AccountId == accountId).ToListAsync();
        }

    }
}
