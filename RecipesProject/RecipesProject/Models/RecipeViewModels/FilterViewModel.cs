namespace RecipesProject.Models.RecipeViewModels
{
    public class FilterViewModel
    {
        public FilterViewModel()
        {
            this.Recipes = new List<RecipeViewModel>();
        }
        public int ServingsFilter { get; set; }
        public string? IngredientFilter { get; set; }   
        public string? TimeFilter { get; set; }
        public List<RecipeViewModel> Recipes { get; set; }   
    }
}
