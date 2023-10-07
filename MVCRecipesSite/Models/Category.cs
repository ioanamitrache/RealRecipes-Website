namespace MVCRecipesSite.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public List<RecipeCategory>? RecipeCategories { get; set; }
    }
}
