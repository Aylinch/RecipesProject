using System.ComponentModel.DataAnnotations;

namespace RecipesProject.Data.Entities
{
    public class Ingredient
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }    
    }
}
