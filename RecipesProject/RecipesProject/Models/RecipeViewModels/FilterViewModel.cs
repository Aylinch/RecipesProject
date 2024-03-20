namespace RecipesProject.Models.RecipeViewModels
{
    public class FilterViewModel
    {
        public int ServingsFilter { get; set; }
        public string? IngredientFilter { get; set; }   
        public string? TimeFilter { get; set; } 
        public List<RecipeViewModel>Recipes { get; set; }   
    }
}
