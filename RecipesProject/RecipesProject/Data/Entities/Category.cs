using System.ComponentModel.DataAnnotations;

namespace RecipesProject.Data.Entities
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; } 
        [Required]  
        public string? Name { get; set; }    
        public string? CategoryImage { get; set; }   
        public HashSet<Recipe> Recipes { get; set; } = new HashSet<Recipe>();    
    }
}
