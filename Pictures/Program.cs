using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Pictures.DAL;
using Pictures.DAL.Interfaces;
using Pictures.DAL.Repositories;
using Pictures.Domain.Entities;
using Pictures.Services.Classes;
using Pictures.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages() // установил пакет Razor.RuntimeCompilation чтобы не перезапускать приложение
	.AddRazorRuntimeCompilation(); // каждый раз после изменений html файлов

builder.Services.AddDbContext<PicturesDbContext>(options =>
		options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme) // подключение авторизации (куки)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString("/Account/Login");
        options.AccessDeniedPath = new PathString("/Account/Login");
    });

builder.Services.AddTransient<IRepository<Picture>, PictureRepository>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>(); // подключение репозитория аккаунта

builder.Services.AddTransient<IPictureService, PictureService>();
builder.Services.AddTransient<IAccountService, AccountService>(); // подключение сервиса аккаунта

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication(); // подключение авторизации

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
