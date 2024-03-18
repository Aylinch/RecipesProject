using RecipesProject.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace RecipesProject.Models.RecipeViewModels
{
    public class RecipeViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Instructions { get; set; }
        public int PrepTime { get; set; }
        public int CookTime { get; set; }
        public int TotalTime { get; set; }
        public int Servings { get; set; }
        public string? Image { get; set; }
        public Guid? CategoryId { get; set; } = null;
        public List<IngredientViewModel>? Ingredients { get; set; }
    }
}
