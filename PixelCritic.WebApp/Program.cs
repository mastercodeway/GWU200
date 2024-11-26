using Microsoft.EntityFrameworkCore;
using PixelCritic.WebApp.Repositories;
using PixelCritic.WebApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace PixelCritic.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddDbContext<PixelDbContext>(optionBuilder =>
            {
                optionBuilder.EnableSensitiveDataLogging(false);
                optionBuilder.UseSqlite(
                    builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/User/Index");
            // Services
            builder.Services.AddHttpClient<IGameService, GameService>();
            builder.Services.AddTransient<IGameService, GameService>();
            builder.Services.AddTransient<ILoginService, LoginService>();
            builder.Services.AddTransient<IUserRepo, UserRepo>();
            builder.Services.AddTransient<IArticleRepo, ArticleRepo>();
            builder.Services.AddTransient<IReviewRepo, ReviewRepo>();
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
           
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
