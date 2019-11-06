namespace CookBook.Common.Models
{
    public class RecipeIngridientView
    {
        public int RecipeId { get; set; }
        public int IngridientId { get; set; }
        public string Name { get; set; }
        public string Weight { get; set; }
    }
}
