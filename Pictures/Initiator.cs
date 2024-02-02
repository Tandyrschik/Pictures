using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Pictures.DAL;
using Pictures.DAL.Interfaces;
using Pictures.DAL.Repositories;
using Pictures.Services.Classes;
using Pictures.Services.Interfaces;
using System.IO.Abstractions;

namespace Pictures
{
    public static class Initiator
    {
        public static void InitAll(WebApplicationBuilder builder)
        {

            InitServices(builder.Services);
            InitRepositories(builder.Services);
            InitRazorPages(builder.Services);
            InitAuthentication(builder.Services);
            InitDB(builder);
        }

        public static void InitServices(IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPictureService, PictureService>();
            services.AddScoped<IFileManagerService, FileManagerService>();
        }

        public static void InitRepositories(IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IPictureRepository, PictureRepository>();
        }

        public static void InitRazorPages(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages() // установил пакет Razor.RuntimeCompilation чтобы не перезапускать приложение
                .AddRazorRuntimeCompilation(); // каждый раз после изменений html файлов
        }

        public static void InitAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme) // подключение авторизации (куки)
            .AddCookie(options =>
            {
                options.LoginPath = new PathString("/Account/Login");
                options.AccessDeniedPath = new PathString("/Account/Login");
            });
        }

        public static void InitDB(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<PicturesDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        }
    }
}