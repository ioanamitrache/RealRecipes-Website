using Microsoft.EntityFrameworkCore;
using MVCRecipesSite.Models;

namespace MVCRecipesSite.Data
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Recipes.Any())
            {
                context.Recipes.AddRange(
                new Recipe
                {
                    Name = "Frozen Strawberry Lemonade",
                    Ingredients = "ice, frozen strawberries, lemons, sweetened condensed milk",
                    Directions = "In a blender, add the ice, strawberries, lemon juice, and condensed milk. Blend until smooth. Serve immediately.",
                    Calories = 189,
                    PrepTime = 5
                });

                context.SaveChanges();
            }

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category
                    {
                        Name = "Vegetarian"
                    });

                context.SaveChanges();
            }

            if (!context.RecipeCategories.Any())
            {
                context.RecipeCategories.AddRange(
                    new RecipeCategory
                    {
                        RecipeId = 1,
                        CategoryId = 1

                    });

                context.SaveChanges();
            }
        }
    }
}
