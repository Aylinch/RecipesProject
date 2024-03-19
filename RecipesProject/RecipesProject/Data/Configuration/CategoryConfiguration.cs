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
                    Name="Appetizers",
                    CategoryImage="https://cdn.momsdish.com/wp-content/uploads/2020/06/Classic-Bruschetta-Recipe-09-scaled.jpg"
                },
                new Category()
                {
                    Id=new Guid("1f03fbf0-adfa-40fb-bb32-f7944ac4a5cb"),
                    Name="Main dishes",
                    CategoryImage="https://i.pinimg.com/736x/63/2e/07/632e07d2c7c1a33896c9b20e329f32f1.jpg"
                },
                new Category()
                {
                    Id = new Guid("35964891-362e-401d-ad89-5b9c5a98671f"),
                    Name="Salads",
                    CategoryImage ="https://th.bing.com/th/id/OIP.2LT7l2oEDjsqW_SCNg4UEQHaFE?rs=1&pid=ImgDetMain"
                },
                 new Category()
                {
                    Id = new Guid("3fcbca87-d864-4eda-a1c6-217c800dc20d"),
                    Name="Soups",
                    CategoryImage="https://th.bing.com/th/id/R.43fec6c14689033a80b3c7ffdfe31aae?rik=SGXteUsTmafgwQ&riu=http%3a%2f%2fweknowyourdreams.com%2fimages%2fsoup%2fsoup-05.jpg&ehk=6IUgafpKHu6FGbvVfwGNtHQoqYQDUQHN24YEPtBAIcI%3d&risl=&pid=ImgRaw&r=0"
                },
                  new Category()
                {
                    Id = new Guid("b565b246-8790-4dba-9aa8-580ea1077982"),
                    Name="Desserts",
                    CategoryImage="https://th.bing.com/th/id/R.561af1c6a2e6985609dd071112475a76?rik=V%2bJAoJ4hV%2fq%2fow&pid=ImgRaw&r=0"
                },
                   new Category()
                {
                    Id = new Guid("ecb1fbcd-7e68-46b3-bff1-1fa2de282e8b"),
                    Name="Breakfast",
                    CategoryImage="https://www.skygate.co.jp/guide/wp-content/uploads/sites/2/2017/03/1703_006-1-768x471.jpg"
                }
            };
            return categories;
        }

    } 
}
