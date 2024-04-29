using Microsoft.EntityFrameworkCore;
using RushipesWeb_V1.Data;
using RushipesWeb_V1.Repository.IRepository;
using RushipesWeb_V1.Repository;
using System.Globalization;

namespace RushipesWeb_V1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<RushipesDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //builder.Services.AddScoped<IDbInitializer, DbInitializerr>();
            builder.Services.AddScoped<IRecettePostRepository, RecettePostRepository>();
            builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();

            var app = builder.Build();

            // Configuration de la culture par défaut en français
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("fr-FR");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("fr-FR");

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
