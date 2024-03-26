using System.ComponentModel.DataAnnotations;

namespace RecipesProject.Models.RecipeViewModels
{
    public class AddRecipeViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Instructions { get; set; }
        [Required]
        public int PrepTime { get; set; }
        [Required]
        public int CookTime { get; set; }
        [Required]
        public int TotalTime { get; set; }
        [Required]
        public int Servings { get; set; }
        [Required]
        public string? Image { get; set; }
        public Guid? CategoryId { get; set; }    
        public List<IngredientViewModel>?Ingredients { get; set; }  
    }
}
