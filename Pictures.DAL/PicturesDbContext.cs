using Microsoft.EntityFrameworkCore;
using Pictures.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pictures.DAL
{
    public class PicturesDbContext : DbContext
    {
        public PicturesDbContext(DbContextOptions<PicturesDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        public DbSet<Picture> Picture { get; set; }
        public DbSet<Account> Account { get; set; }
    }
}