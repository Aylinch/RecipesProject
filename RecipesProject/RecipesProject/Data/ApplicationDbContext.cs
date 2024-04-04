using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecipesProject.Data.Account;
using RecipesProject.Data.Configuration;
using RecipesProject.Data.Entities;

namespace RecipesProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Recipe> Recipes { get; set; }  
        public DbSet<Ingredient> Ingredients { get; set; }  
        public DbSet<Category> Categories { get; set; } 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new RecipeIngredientConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRolesConfiguration());
            base.OnModelCreating(builder);
        }
    }
}