using MVCRecipesSite.Models;

namespace MVCRecipesSite.Data
{
    //repositories are good for reducing duplications in code
    public interface IRecipeRepository
    {
        IQueryable<Recipe> Recipes { get; }
        IQueryable<Category> Categories { get; }
        IQueryable<RecipeCategory> RecipeCategories { get; }
        Task SaveRecipeAsync(Recipe recipe);
        Task<Recipe> DeleteRecipeAsync(int recipeId);
    }
}
