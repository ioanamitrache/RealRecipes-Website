using MVCRecipesSite.Models;

namespace MVCRecipesSite.Data
{
    public class EFRecipeRepository : IRecipeRepository
    {
        private ApplicationDbContext context;
        public EFRecipeRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Recipe> Recipes
        {
            get { return context.Recipes; }
        }

        public IQueryable<Category> Categories
        {
            get { return context.Categories; }
        }

        public IQueryable<RecipeCategory> RecipeCategories
        {
            get { return context.RecipeCategories; }
        }

        public async Task<Recipe?> DeleteRecipeAsync(int recipeId)
        {
            Recipe? dbEntry = context.Recipes.FirstOrDefault(x => x.RecipeId == recipeId);
            if(dbEntry!=null)
            {
                context.Recipes.Remove(dbEntry);
                await context.SaveChangesAsync();
            }
            return dbEntry;
        }

        public async Task SaveRecipeAsync(Recipe recipe)
        {
            if(recipe.RecipeId == 0)
            {
                context.Recipes.Add(recipe);
            }
            else
            {
                Recipe entity = context.Recipes.FirstOrDefault(x => x.RecipeId == recipe.RecipeId);
                if(entity != null)
                {
                    entity.Name = recipe.Name;
                    entity.Ingredients = recipe.Ingredients;
                    entity.Calories = recipe.Calories;
                    entity.Directions = recipe.Directions;
                    entity.PrepTime = recipe.PrepTime;
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
