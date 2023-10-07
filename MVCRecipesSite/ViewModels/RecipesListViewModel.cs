using MVCRecipesSite.Models;

namespace MVCRecipesSite.ViewModels
{
    public class RecipesListViewModel
    {
        public IEnumerable<Recipe> Recipes { get; set; } = Enumerable.Empty<Recipe>();

        public PagingInfoViewModel PagingInfo { get; set; } = new PagingInfoViewModel();
    }
}
