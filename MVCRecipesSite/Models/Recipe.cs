using System.ComponentModel.DataAnnotations;

namespace MVCRecipesSite.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter the ingredients")]
        public string Ingredients { get; set; }
        [Required(ErrorMessage = "Please enter directions")]
        public string Directions { get; set; }
        [Required(ErrorMessage = "Please enter the aproximate preparation time")]
        public int PrepTime { get; set; }
        [Required(ErrorMessage = "Please enter the number of calories")]
        [Range(1, 10000, ErrorMessage = "Please enter a positive number")]
        public int Calories { get; set; }

        public List<RecipeCategory>? RecipeCategories { get; set; }

    }
}
