using Microsoft.AspNetCore.Mvc;
using MVCRecipesSite.Data;
using MVCRecipesSite.ViewModels;

namespace MVCRecipesSite.Controllers
{
    public class BrowseRecipesController: Controller
    {
        public int PageSize = 1; //the number of recipes/page
        IRecipeRepository repository;
        public BrowseRecipesController(IRecipeRepository repo)
        {
            repository = repo;
        }
        public IActionResult Index(int recipePage = 1)
        {

            ViewBag.Title = "Real Recipes";

            var recipes = repository.Recipes
                .OrderBy(x => x.RecipeId)
                .Skip((recipePage - 1) * PageSize)
                .Take(PageSize);

            var pagingInfo = new PagingInfoViewModel
            {
                CurrentPage = recipePage,
                ItemsPerPage = PageSize,
                TotalItems = repository.Recipes.Count()
            };

            RecipesListViewModel viewModel = new RecipesListViewModel()
            {
                Recipes = recipes,
                PagingInfo = pagingInfo
            };

            return View(viewModel);
        }
    }
}

