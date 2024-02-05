using Microsoft.EntityFrameworkCore;
using Pictures.Domain.Entities;
using Pictures.Domain.Enums;

namespace Pictures.DAL
{
    public class PicturesDbContext : DbContext
    {
        public PicturesDbContext(DbContextOptions<PicturesDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasData(
                new Account { Id = 1, Login = "Alex123", Password = "c775e7b757ede630cd0aa1113bd102661ab38829ca52a6422ab782862f268646", Email = "Alex@mail.com", Name = "Alex", Surname = "Xela", Role = Role.Admin },
                new Account { Id = 2, Login = "Daniel321", Password = "c775e7b757ede630cd0aa1113bd102661ab38829ca52a6422ab782862f268646", Email = "Daniel@mail.com", Name = "Daniel", Surname = "lainad", Role = Role.Admin },
                new Account { Id = 3, Login = "Tandyrschik", Password = "c775e7b757ede630cd0aa1113bd102661ab38829ca52a6422ab782862f268646", Email = "Tandyrschik@gmail.com", Name = "Evgenii", Surname = "Borisovich", Role = Role.DefаultUser },
                new Account { Id = 4, Login = "Oleg123", Password = "c775e7b757ede630cd0aa1113bd102661ab38829ca52a6422ab782862f268646", Email = "Oleg@gmail.com", Name = "Oleg", Surname = "Fedorovich", Role = Role.DefаultUser });

            modelBuilder.Entity<Picture>().HasData(
                new Picture { Id = 1, Address = "/img/lotos.jpg", Name = "Lotos", AccountId = 1 },
                new Picture { Id = 2, Address = "/img/night city.jpg", Name = "Night city", AccountId = 1 },
                new Picture { Id = 3, Address = "/img/puppy.jpg", Name = "Puppy", AccountId = 1 },
                new Picture { Id = 4, Address = "/img/snowflake.jpg", Name = "Snowflake", AccountId = 1 },
                new Picture { Id = 5, Address = "/img/misty forest.jpg", Name = "Misty forest", AccountId = 1 },
                new Picture { Id = 6, Address = "/img/rabbit.jpg", Name = "Rabbit", AccountId = 1 },
                new Picture { Id = 7, Address = "/img/tulip.jpg", Name = "Tulip", AccountId = 2 },
                new Picture { Id = 8, Address = "/img/zurich.jpg", Name = "Zurich", AccountId = 2 },
                new Picture { Id = 9, Address = "/img/evening sea.jpg", Name = "Evening sea", AccountId = 2 },
                new Picture { Id = 10, Address = "/img/bear.jpg", Name = "Bear", AccountId = 2 },
                new Picture { Id = 11, Address = "/img/cat.jpg", Name = "Cat", AccountId = 2 },
                new Picture { Id = 12, Address = "/img/helix nebula.jpg", Name = "Helix nebula", AccountId = 2 },
                new Picture { Id = 13, Address = "/img/photo_2023-10-05_07-57-38_51d3b0f7-a_11_11_2023_23_23_10.jpg", Name = "Kitten", AccountId = 3 },
                new Picture { Id = 14, Address = "/img/photo tulips_ad8ecd9d-f_13_12_2023_11_59_06.png", Name = "Tulips", AccountId = 4 });
        }
        public DbSet<Picture> Picture { get; set; }
        public DbSet<Account> Account { get; set; }
    }
}