using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipesProject.Migrations
{
    public partial class Seed2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IngredientQuanitity",
                table: "RecipeIngredients",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CategoryImage",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6383851-4451-46c9-8d99-252c81e153ee",
                column: "ConcurrencyStamp",
                value: "12bcd4ba-51e7-4f8a-bf83-e8e26145319a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "52ff7a8f-b2b1-4a92-9fa6-92785311d879",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "581477bf-9f88-40c3-a999-50b7f6d2be02", "AQAAAAEAACcQAAAAEEUzUCgJBPhJRdvTjqZsuQwPXLrIUMuckIvx46srSkpNy+8/0b/cKZeciwPg/z4Bkg==", "ae6dc982-f143-415a-b378-4f7b89a155e8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6b243550-7cc1-4d75-8064-cef4c3d8be35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "881d6cac-f77c-41d6-901b-77f5115f68ac", "AQAAAAEAACcQAAAAEJFvDpkqHX7XtHxokFKPlsdJZmVrPZKZULDwf2QC9/tGbzPIedHy3huwZQU/zD/XGg==", "80ddfc15-da97-4c6a-8f89-83cdbba42b15" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1f03fbf0-adfa-40fb-bb32-f7944ac4a5cb"),
                column: "CategoryImage",
                value: "https://i.pinimg.com/736x/63/2e/07/632e07d2c7c1a33896c9b20e329f32f1.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("35964891-362e-401d-ad89-5b9c5a98671f"),
                column: "CategoryImage",
                value: "https://th.bing.com/th/id/OIP.2LT7l2oEDjsqW_SCNg4UEQHaFE?rs=1&pid=ImgDetMain");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3fcbca87-d864-4eda-a1c6-217c800dc20d"),
                column: "CategoryImage",
                value: "https://th.bing.com/th/id/R.43fec6c14689033a80b3c7ffdfe31aae?rik=SGXteUsTmafgwQ&riu=http%3a%2f%2fweknowyourdreams.com%2fimages%2fsoup%2fsoup-05.jpg&ehk=6IUgafpKHu6FGbvVfwGNtHQoqYQDUQHN24YEPtBAIcI%3d&risl=&pid=ImgRaw&r=0");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("663f7ebb-fcfb-4f3e-a1d4-c89f6f9c627f"),
                column: "CategoryImage",
                value: "https://cdn.momsdish.com/wp-content/uploads/2020/06/Classic-Bruschetta-Recipe-09-scaled.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b565b246-8790-4dba-9aa8-580ea1077982"),
                column: "CategoryImage",
                value: "https://th.bing.com/th/id/R.561af1c6a2e6985609dd071112475a76?rik=V%2bJAoJ4hV%2fq%2fow&pid=ImgRaw&r=0");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ecb1fbcd-7e68-46b3-bff1-1fa2de282e8b"),
                column: "CategoryImage",
                value: "https://www.skygate.co.jp/guide/wp-content/uploads/sites/2/2017/03/1703_006-1-768x471.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryImage",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "IngredientQuanitity",
                table: "RecipeIngredients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6383851-4451-46c9-8d99-252c81e153ee",
                column: "ConcurrencyStamp",
                value: "19612566-c03f-4abc-980b-1176e24d1c54");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "52ff7a8f-b2b1-4a92-9fa6-92785311d879",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "88d6c38e-64b0-4d03-9b25-1fcd3b81c77a", "AQAAAAEAACcQAAAAEBq6nq7DkneWVyZVCDf8pOSVWWfrtTecAF7SbCEK4Mcpbb2FzRNMB32oY3zXca2rvg==", "c491808c-24a1-4247-b9da-be3b949d1397" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6b243550-7cc1-4d75-8064-cef4c3d8be35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "addc3662-5f98-41cf-9dc1-812363ff50a9", "AQAAAAEAACcQAAAAEMZ+RwRNK8L9dbrQ4DXIXdVtjOaIWu6mB/0xO7G7jsU9G9Rv+DYpZnKN0bBo2fG+eg==", "677305d0-9537-4be4-a1a9-23f8b549be78" });
        }
    }
}
