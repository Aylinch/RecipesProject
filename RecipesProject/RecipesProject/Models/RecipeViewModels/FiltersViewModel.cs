using RecipesProject.Data.Entities;

namespace RecipesProject.Models.RecipeViewModels
{
    public class FiltersViewModel
    {
        public string? ServingsFilter { get; set; }
        public string? IngredientFilter { get; set; }
        public string? TimeFilters { get; set; }
        public bool Chicken { get; set; }
        public bool Pork { get; set; }
        public bool Beef { get; set; }
        public bool Pasta { get; set; }
        public List<RecipeViewModel> Recipes { get; set; }
    }
}
