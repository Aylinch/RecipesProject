using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipesProject.Data.Entities;

namespace RecipesProject.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(CreateCategory());
        }
        private static List<Category> CreateCategory()
        {
            List<Category> categories = new List<Category>()
            {
                new Category()
                {
                    Id = new Guid("663f7ebb-fcfb-4f3e-a1d4-c89f6f9c627f"),
                    Name="Appetizers"
                },
                new Category()
                {
                    Id=new Guid("1f03fbf0-adfa-40fb-bb32-f7944ac4a5cb"),
                    Name="Main dishes"
                },
                new Category()
                {
                    Id = new Guid("35964891-362e-401d-ad89-5b9c5a98671f"),
                    Name="Salads"
                },
                 new Category()
                {
                    Id = new Guid("3fcbca87-d864-4eda-a1c6-217c800dc20d"),
                    Name="Soups"
                },
                  new Category()
                {
                    Id = new Guid("b565b246-8790-4dba-9aa8-580ea1077982"),
                    Name="Desserts"
                },
                   new Category()
                {
                    Id = new Guid("ecb1fbcd-7e68-46b3-bff1-1fa2de282e8b"),
                    Name="Breakfast"
                }
            };
            return categories;
        }

    } 
}
