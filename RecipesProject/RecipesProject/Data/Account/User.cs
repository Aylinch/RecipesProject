using Microsoft.AspNetCore.Identity;
using RecipesProject.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace RecipesProject.Data.Account
{
    public class User : IdentityUser
    {


        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        public int Age { get; set; }

        public HashSet<Recipe> Recipes { get; set; } = new HashSet<Recipe>();
    }
}
