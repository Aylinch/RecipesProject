namespace RecipesProject.Models.RecipeViewModels
{
    public class IngredientViewModel
    {
        public int Index { get; set; }
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Quantity { get; set; }    
    }
}
