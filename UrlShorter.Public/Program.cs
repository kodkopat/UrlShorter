using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using UrlShorter.Application.Services;
using UrlShorter.Application.Services.Interfaces;
using UrlShorter.Domain.Interfaces;
using UrlShorter.Domain.Interfaces.Repositories;
using UrlShorter.Infrastructure;
using UrlShorter.Infrastructure.Persistence;
using UrlShorter.Infrastructure.Repositories;

namespace UrlShorter.Public
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(c =>
            {
                c.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IUrlRepository, UrlRepository>();
            builder.Services.AddScoped<IUrlService, UrlService>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute(
            name: "ShortUrl",
            pattern: "{key}",
            defaults: new { controller = "Url", action = "RedirectUrl" });
            UpdateDatabase(app);
            app.Run();

            static void UpdateDatabase(IHost app)
            {
                using var scope = app.Services.CreateScope();
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();
            }
        }
    }
}
