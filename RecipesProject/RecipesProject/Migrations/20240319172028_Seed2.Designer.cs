﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecipesProject.Data;

#nullable disable

namespace RecipesProject.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240319172028_Seed2")]
    partial class Seed2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "a6383851-4451-46c9-8d99-252c81e153ee",
                            ConcurrencyStamp = "12bcd4ba-51e7-4f8a-bf83-e8e26145319a",
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "6b243550-7cc1-4d75-8064-cef4c3d8be35",
                            RoleId = "a6383851-4451-46c9-8d99-252c81e153ee"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("RecipesProject.Data.Account.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<Guid?>("RecipeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("RecipeId");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "52ff7a8f-b2b1-4a92-9fa6-92785311d879",
                            AccessFailedCount = 0,
                            Age = 19,
                            ConcurrencyStamp = "581477bf-9f88-40c3-a999-50b7f6d2be02",
                            Email = "limoni@abv.bg",
                            EmailConfirmed = false,
                            FirstName = "Simona",
                            LastName = "Palieva",
                            LockoutEnabled = false,
                            NormalizedEmail = "LIMONI@ABV.BG",
                            NormalizedUserName = "MONIO",
                            PasswordHash = "AQAAAAEAACcQAAAAEEUzUCgJBPhJRdvTjqZsuQwPXLrIUMuckIvx46srSkpNy+8/0b/cKZeciwPg/z4Bkg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "ae6dc982-f143-415a-b378-4f7b89a155e8",
                            TwoFactorEnabled = false,
                            UserName = "Monio"
                        },
                        new
                        {
                            Id = "6b243550-7cc1-4d75-8064-cef4c3d8be35",
                            AccessFailedCount = 0,
                            Age = 20,
                            ConcurrencyStamp = "881d6cac-f77c-41d6-901b-77f5115f68ac",
                            Email = "aylin@abv.bg",
                            EmailConfirmed = false,
                            FirstName = "Aylin",
                            LastName = "Chakakchi",
                            LockoutEnabled = false,
                            NormalizedEmail = "AYLIN@ABV.BG",
                            NormalizedUserName = "AYLINN",
                            PasswordHash = "AQAAAAEAACcQAAAAEJFvDpkqHX7XtHxokFKPlsdJZmVrPZKZULDwf2QC9/tGbzPIedHy3huwZQU/zD/XGg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "80ddfc15-da97-4c6a-8f89-83cdbba42b15",
                            TwoFactorEnabled = false,
                            UserName = "Aylinn"
                        });
                });

            modelBuilder.Entity("RecipesProject.Data.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("663f7ebb-fcfb-4f3e-a1d4-c89f6f9c627f"),
                            CategoryImage = "https://cdn.momsdish.com/wp-content/uploads/2020/06/Classic-Bruschetta-Recipe-09-scaled.jpg",
                            Name = "Appetizers"
                        },
                        new
                        {
                            Id = new Guid("1f03fbf0-adfa-40fb-bb32-f7944ac4a5cb"),
                            CategoryImage = "https://i.pinimg.com/736x/63/2e/07/632e07d2c7c1a33896c9b20e329f32f1.jpg",
                            Name = "Main dishes"
                        },
                        new
                        {
                            Id = new Guid("35964891-362e-401d-ad89-5b9c5a98671f"),
                            CategoryImage = "https://th.bing.com/th/id/OIP.2LT7l2oEDjsqW_SCNg4UEQHaFE?rs=1&pid=ImgDetMain",
                            Name = "Salads"
                        },
                        new
                        {
                            Id = new Guid("3fcbca87-d864-4eda-a1c6-217c800dc20d"),
                            CategoryImage = "https://th.bing.com/th/id/R.43fec6c14689033a80b3c7ffdfe31aae?rik=SGXteUsTmafgwQ&riu=http%3a%2f%2fweknowyourdreams.com%2fimages%2fsoup%2fsoup-05.jpg&ehk=6IUgafpKHu6FGbvVfwGNtHQoqYQDUQHN24YEPtBAIcI%3d&risl=&pid=ImgRaw&r=0",
                            Name = "Soups"
                        },
                        new
                        {
                            Id = new Guid("b565b246-8790-4dba-9aa8-580ea1077982"),
                            CategoryImage = "https://th.bing.com/th/id/R.561af1c6a2e6985609dd071112475a76?rik=V%2bJAoJ4hV%2fq%2fow&pid=ImgRaw&r=0",
                            Name = "Desserts"
                        },
                        new
                        {
                            Id = new Guid("ecb1fbcd-7e68-46b3-bff1-1fa2de282e8b"),
                            CategoryImage = "https://www.skygate.co.jp/guide/wp-content/uploads/sites/2/2017/03/1703_006-1-768x471.jpg",
                            Name = "Breakfast"
                        });
                });

            modelBuilder.Entity("RecipesProject.Data.Entities.Ingredient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("RecipesProject.Data.Entities.Recipe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CookTime")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Instructions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PrepTime")
                        .HasColumnType("int");

                    b.Property<int>("Servings")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalTime")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("RecipesProject.Data.Entities.RecipeIngredients", b =>
                {
                    b.Property<Guid>("IngredientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RecipeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("IngredientQuanitity")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IngredientId", "RecipeId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeIngredients");
                });

            modelBuilder.Entity("RecipesProject.Data.Entities.RecipeUser", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("RecipeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.HasKey("UserId", "RecipeId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("RecipesProject.Data.Account.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("RecipesProject.Data.Account.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipesProject.Data.Account.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("RecipesProject.Data.Account.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RecipesProject.Data.Account.User", b =>
                {
                    b.HasOne("RecipesProject.Data.Entities.Recipe", null)
                        .WithMany("Users")
                        .HasForeignKey("RecipeId");
                });

            modelBuilder.Entity("RecipesProject.Data.Entities.Recipe", b =>
                {
                    b.HasOne("RecipesProject.Data.Entities.Category", "Category")
                        .WithMany("Recipes")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("RecipesProject.Data.Entities.RecipeIngredients", b =>
                {
                    b.HasOne("RecipesProject.Data.Entities.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipesProject.Data.Entities.Recipe", "Recipe")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("RecipesProject.Data.Entities.RecipeUser", b =>
                {
                    b.HasOne("RecipesProject.Data.Entities.Recipe", "Recipe")
                        .WithMany()
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipesProject.Data.Account.User", "User")
                        .WithMany("RecipeUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RecipesProject.Data.Account.User", b =>
                {
                    b.Navigation("RecipeUsers");
                });

            modelBuilder.Entity("RecipesProject.Data.Entities.Category", b =>
                {
                    b.Navigation("Recipes");
                });

            modelBuilder.Entity("RecipesProject.Data.Entities.Recipe", b =>
                {
                    b.Navigation("RecipeIngredients");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
