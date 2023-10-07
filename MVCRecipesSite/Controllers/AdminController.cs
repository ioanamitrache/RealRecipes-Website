using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCRecipesSite.Data;
using MVCRecipesSite.Models;

namespace MVCRecipesSite.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IRecipeRepository repository;
        public AdminController(IRecipeRepository repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            var recipes = repository.Recipes;
            return View(recipes);
        }

        [HttpGet]
        public IActionResult Edit(int recipeId)
        {
            var recipe = repository.Recipes.FirstOrDefault(r => r.RecipeId == recipeId);
            return View(recipe);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Recipe recipe)
        {
            if(ModelState.IsValid)
            {
                await repository.SaveRecipeAsync(recipe);
                TempData["message"] = "Product updated!";
                return RedirectToAction("Index");
            }
            else
            {
                return View(recipe);
            }
        }

        public IActionResult Create()
        {
            return View("Edit", new Recipe());
        }

        [HttpPost]
        [Authorize(Roles ="DeleteRecipesRole")]
        public async Task<IActionResult> Delete(int recipeId)
        {
            Recipe recipe = await repository.DeleteRecipeAsync(recipeId);
            if(recipe != null)
            {
                TempData["message"] = $"{recipe.Name} was deleted.";
            }
            return RedirectToAction("Index");
        }
    }
}
