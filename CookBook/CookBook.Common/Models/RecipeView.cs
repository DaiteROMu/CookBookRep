namespace CookBook.Common.Models
{
    public class RecipeView
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
