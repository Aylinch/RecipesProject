using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecipesProject.Data.Account;
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
            base.OnModelCreating(builder);
            builder.Entity<RecipeIngredients>()
                .HasKey(x => new { x.IngredientId, x.RecipeId });
            builder.Entity<RecipeUser>()
                .HasKey(x => new { x.UserId, x.RecipeId });
            builder.Entity<Category>()
                .HasData(new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Breakfast",
                },
                new Category()
                {
                    Id= Guid.NewGuid(),
                    Name="Apetizer"
                },
                new Category()
                {
                    Id=Guid.NewGuid(),  
                    Name="Soup",
                },
                 new Category()
                 {
                     Id = Guid.NewGuid(),
                     Name = "Salad",
                 },
                  new Category()
                  {
                      Id = Guid.NewGuid(),
                      Name = "Dessert"
                  },
                   new Category()
                   {
                       Id = Guid.NewGuid(),
                       Name = "Main dish "
                   }
                );
        }
    }
}