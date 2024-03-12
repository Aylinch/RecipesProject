using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipesProject.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0f340dfe-e181-4001-8e40-1602eacb42f7"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("249d47fb-bb25-45c3-bc04-43ccbe4005b6"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("748f2fe4-72ae-4663-9c98-52a41c24777f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7a378c4d-0abd-41a7-99e7-38d8c62634e8"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a00aea51-a6de-4d8d-98cf-793508565d64"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("fbeb2eda-cb06-4592-a156-2db8126bb0d7"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a6383851-4451-46c9-8d99-252c81e153ee", "19612566-c03f-4abc-980b-1176e24d1c54", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Age", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RecipeId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "52ff7a8f-b2b1-4a92-9fa6-92785311d879", 0, 19, "88d6c38e-64b0-4d03-9b25-1fcd3b81c77a", "limoni@abv.bg", false, "Simona", "Palieva", false, null, "LIMONI@ABV.BG", "MONIO", "AQAAAAEAACcQAAAAEBq6nq7DkneWVyZVCDf8pOSVWWfrtTecAF7SbCEK4Mcpbb2FzRNMB32oY3zXca2rvg==", null, false, null, "c491808c-24a1-4247-b9da-be3b949d1397", false, "Monio" },
                    { "6b243550-7cc1-4d75-8064-cef4c3d8be35", 0, 20, "addc3662-5f98-41cf-9dc1-812363ff50a9", "aylin@abv.bg", false, "Aylin", "Chakakchi", false, null, "AYLIN@ABV.BG", "AYLINN", "AQAAAAEAACcQAAAAEMZ+RwRNK8L9dbrQ4DXIXdVtjOaIWu6mB/0xO7G7jsU9G9Rv+DYpZnKN0bBo2fG+eg==", null, false, null, "677305d0-9537-4be4-a1a9-23f8b549be78", false, "Aylinn" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1f03fbf0-adfa-40fb-bb32-f7944ac4a5cb"), "Main dishes" },
                    { new Guid("35964891-362e-401d-ad89-5b9c5a98671f"), "Salads" },
                    { new Guid("3fcbca87-d864-4eda-a1c6-217c800dc20d"), "Soups" },
                    { new Guid("663f7ebb-fcfb-4f3e-a1d4-c89f6f9c627f"), "Appetizers" },
                    { new Guid("b565b246-8790-4dba-9aa8-580ea1077982"), "Desserts" },
                    { new Guid("ecb1fbcd-7e68-46b3-bff1-1fa2de282e8b"), "Breakfast" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a6383851-4451-46c9-8d99-252c81e153ee", "6b243550-7cc1-4d75-8064-cef4c3d8be35" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a6383851-4451-46c9-8d99-252c81e153ee", "6b243550-7cc1-4d75-8064-cef4c3d8be35" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "52ff7a8f-b2b1-4a92-9fa6-92785311d879");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1f03fbf0-adfa-40fb-bb32-f7944ac4a5cb"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("35964891-362e-401d-ad89-5b9c5a98671f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3fcbca87-d864-4eda-a1c6-217c800dc20d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("663f7ebb-fcfb-4f3e-a1d4-c89f6f9c627f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b565b246-8790-4dba-9aa8-580ea1077982"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ecb1fbcd-7e68-46b3-bff1-1fa2de282e8b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6383851-4451-46c9-8d99-252c81e153ee");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6b243550-7cc1-4d75-8064-cef4c3d8be35");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0f340dfe-e181-4001-8e40-1602eacb42f7"), "Apetizer" },
                    { new Guid("249d47fb-bb25-45c3-bc04-43ccbe4005b6"), "Breakfast" },
                    { new Guid("748f2fe4-72ae-4663-9c98-52a41c24777f"), "Soup" },
                    { new Guid("7a378c4d-0abd-41a7-99e7-38d8c62634e8"), "Salad" },
                    { new Guid("a00aea51-a6de-4d8d-98cf-793508565d64"), "Dessert" },
                    { new Guid("fbeb2eda-cb06-4592-a156-2db8126bb0d7"), "Main dish " }
                });
        }
    }
}
