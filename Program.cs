using Microsoft.EntityFrameworkCore;
using SSS_StoreStockSystem.BLL.Interfaces;
using SSS_StoreStockSystem.BLL.Repositories;
using SSS_StoreStockSystem.BLL.UnitOfWork;
using SSS_StoreStockSystem.DAL.Data;
using SSS_StoreStockSystem.DAL.Data.Seeding;
using SSS_StoreStockSystem.Helpers;

namespace SSS_StoreStockSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            ContextSeed.SeedAsync(builder.Services.BuildServiceProvider().GetRequiredService<AppDBContext>());

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IStockRepository, StockRepository>();
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));


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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
