using Microsoft.EntityFrameworkCore;
using Pictures.DAL.Interfaces;
using Pictures.Domain.Entities;

namespace Pictures.DAL.Repositories
{
	public class PictureRepository : IPictureRepository // индивидуальный класс-обработчик запросов в БД для объектов Picture
	{                                                  // обращается в БД посредством EntityFramework
		private readonly PicturesDbContext _context;
		public PictureRepository(PicturesDbContext context)
		{
			_context = context;
		}
		public bool Add(Picture picture) 
		{
			_context.Add(picture);
			_context.SaveChanges();

			return true;
		}

		public bool Remove(Picture picture)
		{
			_context.Remove(picture);
			_context.SaveChanges();

			return true;
		}
		public Picture GetById(int id)
		{
			return _context.Picture.FirstOrDefault(p => p.Id == id);
		}
		public IEnumerable<Picture> GetAll()
		{
			return _context.Picture.ToList();
		}


	}
}
