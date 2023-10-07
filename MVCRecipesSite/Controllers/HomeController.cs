using Microsoft.AspNetCore.Mvc;
using MVCRecipesSite.Data;

namespace MVCRecipesSite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            ViewBag.Title = "Real Recipes";
            return View();
        }
    }
}
