using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipesProject.Data.Entities;

namespace RecipesProject.Data.Configuration
{
    public class RecipeIngredientConfiguration : IEntityTypeConfiguration<RecipeIngredients>
    {
        public void Configure(EntityTypeBuilder<RecipeIngredients> builder)
        {
            builder.HasKey(x => new { x.IngredientId, x.RecipeId });
        }
    }
}
