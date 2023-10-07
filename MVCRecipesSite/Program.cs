using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCRecipesSite.Data;

namespace MVCRecipesSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //the app is going to be able to use controllers and their associated views
            builder.Services.AddControllersWithViews();

            string connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
            builder.Services.AddDbContext<ApplicationDbContext>(opts => opts.UseSqlServer(connectionString));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddScoped<IRecipeRepository, EFRecipeRepository>();

            //Configurations
            var app = builder.Build();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllerRoute("pagination", "Recipes/BrowseRecipes/Page{recipePage}",
                              new { Controller = "BrowseRecipes", action = "Index" });

            app.MapDefaultControllerRoute(); //mapping the url to the controller actions

            app.MapRazorPages();

            SeedData.EnsurePopulated(app);
            Task.Run(async () =>
            await SeedDataIdentity.EnsurePopulatedAsync(app)
            ).Wait();

            app.Run();

        }
    }
}

