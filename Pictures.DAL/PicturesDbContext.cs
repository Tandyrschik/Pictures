using Microsoft.EntityFrameworkCore;
using Pictures.Domain.Entities;

namespace Pictures.DAL
{
	public class PicturesDbContext : DbContext // класс для запросов в БД, базовый класс предоставлен Entity Framework
	{
		public PicturesDbContext(DbContextOptions<PicturesDbContext> options) : base(options) { }

		public DbSet<Picture> Picture { get; set; } // каждое поле DbSet<T> - таблица в БД, колонки таблицы описываются в классе <T>
		public DbSet<User> User { get; set; }
	}
}