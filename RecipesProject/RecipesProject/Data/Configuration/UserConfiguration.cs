using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipesProject.Data.Account;

namespace RecipesProject.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(CreateUser());
        }
        private List<User>CreateUser()
        {
            var users = new List<User>();
            var hasher = new PasswordHasher<User>();

            var user = new User()
            {
                Id = "52ff7a8f-b2b1-4a92-9fa6-92785311d879",
                UserName = "Monio", 
                FirstName="Simona",
                LastName="Palieva",
                Age=19,
                NormalizedUserName = "MONIO",
                Email = "limoni@abv.bg",
                NormalizedEmail = "LIMONI@ABV.BG",
            };
            user.PasswordHash = hasher.HashPassword(user, "Limoni.12");
            users.Add(user);
            var user1 = new User()
            {
                Id = "6b243550-7cc1-4d75-8064-cef4c3d8be35",
                UserName = "Aylinn",
                FirstName="Aylin",
                LastName="Chakakchi",
                Age=20,
                NormalizedUserName = "AYLINN",
                Email = "aylin@abv.bg",
                NormalizedEmail = "AYLIN@ABV.BG",
            };
            user1.PasswordHash = hasher.HashPassword(user1, "Aylin.12");
            users.Add(user1);
            return users;   
        }
    }
}
