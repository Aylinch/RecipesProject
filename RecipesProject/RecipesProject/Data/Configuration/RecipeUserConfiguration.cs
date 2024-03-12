using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipesProject.Data.Entities;

namespace RecipesProject.Data.Configuration
{
    public class RecipeUserConfiguration : IEntityTypeConfiguration<RecipeUser>
    {
        public void Configure(EntityTypeBuilder<RecipeUser> builder)
        {
            builder.HasKey(x => new { x.UserId, x.RecipeId });
        }
    }
}
