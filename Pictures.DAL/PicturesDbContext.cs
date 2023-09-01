using Microsoft.EntityFrameworkCore;
using Pictures.DAL.Repositories;
using Pictures.Domain.Entities;
using System;
using System.Reflection.Emit;

namespace Pictures.DAL
{
    public class PicturesDbContext : DbContext // класс для запросов в БД, базовый класс предоставлен Entity Framework
    {
        public PicturesDbContext(DbContextOptions<PicturesDbContext> options) : base(options)
        {
            //Database.EnsureCreated(); // нужно создать локальную бд на моём втором рабочем месте
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>().HasData(new User { Id = 1, Login = "Alex123", Password = "12345", Name = "Alex", Surname = "Xela", Email = "Alex@mail.com" });
        //    modelBuilder.Entity<User>().HasData(new User { Id = 2, Login = "Daniel321", Password = "54321", Name = "Daniel", Surname = "Lainad", Email = "Daniel@mail.com" });

        //    modelBuilder.Entity<Picture>().HasData(new Picture { Id = 1, Address = "/img/lotos.jpg", Name = "Lotos", UserId = 1 });
        //    modelBuilder.Entity<Picture>().HasData(new Picture { Id = 2, Address = "/img/night city.jpg", Name = "Night city", UserId = 1 });
        //    modelBuilder.Entity<Picture>().HasData(new Picture { Id = 3, Address = "/img/puppy.jpg", Name = "Puppy", UserId = 1 });
        //    modelBuilder.Entity<Picture>().HasData(new Picture { Id = 4, Address = "/img/snowflake.jpg", Name = "Snowflake", UserId = 1 });
        //    modelBuilder.Entity<Picture>().HasData(new Picture { Id = 5, Address = "/img/misty forest.jpg", Name = "Misty forest", UserId = 1 });
        //    modelBuilder.Entity<Picture>().HasData(new Picture { Id = 6, Address = "/img/rabbit.jpg", Name = "Rabbit", UserId = 1 });

        //    modelBuilder.Entity<Picture>().HasData(new Picture { Id = 7, Address = "/img/tulip.jpg", Name = "Tulip", UserId = 2 });
        //    modelBuilder.Entity<Picture>().HasData(new Picture { Id = 8, Address = "/img/zurich.jpg", Name = "Zurich", UserId = 2 });
        //    modelBuilder.Entity<Picture>().HasData(new Picture { Id = 9, Address = "/img/evening sea.jpg", Name = "Evening sea", UserId = 2 });
        //    modelBuilder.Entity<Picture>().HasData(new Picture { Id = 10, Address = "/img/bear.jpg", Name = "Bear", UserId = 2 });
        //    modelBuilder.Entity<Picture>().HasData(new Picture { Id = 11, Address = "/img/cat.jpg", Name = "Cat", UserId = 2 });
        //    modelBuilder.Entity<Picture>().HasData(new Picture { Id = 12, Address = "/img/helix nebula.jpg", Name = "Helix nebula", UserId = 2 });
        //}
        public DbSet<Picture> Picture { get; set; } // каждое поле DbSet<T> - таблица в БД, колонки таблицы описываются в классе <T>
        public DbSet<User> User { get; set; }
    }
}