using System.ComponentModel.DataAnnotations.Schema;

namespace RecipesProject.Data.Entities
{
    public class RecipeIngredients
    {
        [ForeignKey(nameof(Recipe))]
        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        [ForeignKey(nameof(Ingredient))]
        public Guid IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        public string IngredientQuanitity { get; set; }
    }
}
