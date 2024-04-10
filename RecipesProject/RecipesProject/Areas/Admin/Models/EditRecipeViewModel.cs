using RecipesProject.Models;
using RecipesProject.Models.RecipeViewModels;

namespace RecipesProject.Areas.Admin.Models
{
    public class EditRecipeViewModel
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
        public Guid? CategoryId { get; set; }
        public List<CategoryViewModel>? Categories { get; set; }
        public List<IngredientViewModel>? Ingredients { get; set; }
        public string? CategoryName { get; internal set; }
        public string? CreatorId { get; set; }
    }
}
