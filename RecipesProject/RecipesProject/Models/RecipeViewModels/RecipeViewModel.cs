using RecipesProject.Data.Account;
using RecipesProject.Data.Entities;

namespace RecipesProject.Models.RecipeViewModels
{
    public class RecipeViewModel
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Instructions { get; set; }
        public int PrepTime { get; set; }
        public int CookTime { get; set; }
        public int TotalTime { get; set; }
        public int Servings { get; set; }
        public string? Image { get; set; }
        public Guid? CategoryId { get; set; } = null;
        public HashSet<RecipeIngredients> RecipeIngredients { get; set; } = new HashSet<RecipeIngredients>();
        public HashSet<Ingredient> Ingredients { get; set; }= new HashSet<Ingredient>();
    }
}
