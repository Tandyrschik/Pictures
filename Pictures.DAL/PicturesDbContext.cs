using Microsoft.EntityFrameworkCore;
using Pictures.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pictures.DAL
{
    public class PicturesDbContext : DbContext // класс для запросов в БД, базовый класс предоставлен Entity Framework
    {
        public PicturesDbContext(DbContextOptions<PicturesDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        public DbSet<Picture> Picture { get; set; } // каждое поле DbSet<T> - таблица в БД, колонки таблицы описываются в классе <T>
        public DbSet<Account> Account { get; set; }
    }
}