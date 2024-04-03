using RecipesProject.Data.Account;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipesProject.Data.Entities
{
    public class RecipeUser
    {
        [ForeignKey(nameof(Recipe))]
        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; } = null!;
        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
