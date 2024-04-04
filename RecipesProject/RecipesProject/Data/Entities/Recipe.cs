using RecipesProject.Data.Account;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipesProject.Data.Entities
{
    public class Recipe
    {
        [Key]
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
        public bool IsApproved { get; set; } = false;
        public Guid? CategoryId { get; set; } = null;
        public Category? Category { get; set; }

        [ForeignKey(nameof(User))]
        public string CreatorId { get; set; }
        public User User { get; set; } = null!;
        public HashSet<RecipeIngredients> RecipeIngredients { get; set; } = new HashSet<RecipeIngredients>(); 
        
    }
}
