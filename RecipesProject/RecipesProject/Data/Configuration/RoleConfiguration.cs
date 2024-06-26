﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RecipesProject.Data.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(Roles());
    }
    private static List<IdentityRole> Roles()
    {
        return new List<IdentityRole>()
            {
                new()
                {
                    Id = "a6383851-4451-46c9-8d99-252c81e153ee",
                    Name = "Admin"
                },
                new()
                {
                    Id="5619007e-b37b-494b-9681-94f4fb9ec279",
                    Name = "User"
                }
            };
    }
}
