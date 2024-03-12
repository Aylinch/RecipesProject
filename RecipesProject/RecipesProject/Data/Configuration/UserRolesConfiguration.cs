using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RecipesProject.Data.Configuration
{
    public class UserRolesConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(new IdentityUserRole<string>()
            {
                RoleId = "a6383851-4451-46c9-8d99-252c81e153ee",
                UserId = "6b243550-7cc1-4d75-8064-cef4c3d8be35",
            });
        }
    }
}
