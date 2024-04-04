using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipesProject.Migrations
{
    public partial class Migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Recipes_RecipeId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "RecipeUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RecipeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Recipes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6383851-4451-46c9-8d99-252c81e153ee",
                column: "ConcurrencyStamp",
                value: "7b3f9fbe-2f6a-4161-838d-c44fc4651ef6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "52ff7a8f-b2b1-4a92-9fa6-92785311d879",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "30027618-81b6-4e77-86a6-7e97c665b327", "AQAAAAEAACcQAAAAEGeeTYqENsZlrd2eoGz68dy9yOeLvUfQJzxqTIkqqA4tw6sdvv5CrlBNUEDUMMCDqg==", "561541cd-b648-4961-aa95-a45da193b468" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6b243550-7cc1-4d75-8064-cef4c3d8be35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "31780fa5-8d24-41a1-b046-6154dc9908d9", "AQAAAAEAACcQAAAAEJjjxG0B4nCnmMcvgLsoVYUSSbx9ELCye/EUeUyb3VYAsyiEJFK8+92/Jhc7ReO01A==", "11496b60-2841-4761-97a8-4dc1928b95c5" });

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_CreatorId",
                table: "Recipes",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_CreatorId",
                table: "Recipes",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_CreatorId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_CreatorId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Recipes");

            migrationBuilder.AddColumn<Guid>(
                name: "RecipeId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RecipeUsers",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeUsers", x => new { x.UserId, x.RecipeId });
                    table.ForeignKey(
                        name: "FK_RecipeUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeUsers_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6383851-4451-46c9-8d99-252c81e153ee",
                column: "ConcurrencyStamp",
                value: "40749999-5fa2-4811-8244-4899fe779e61");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "52ff7a8f-b2b1-4a92-9fa6-92785311d879",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "efcd0d2a-5518-4c5d-b1ad-97e36e9d39b9", "AQAAAAEAACcQAAAAEPGxeNOJu3q+SxwZGNVvpQVG5FYEuotPpBaJMD0p6f7+Go7IUI7PXcwqY2RT2y3New==", "e269f39d-0c5b-4a36-8fad-0e9f3938ce8d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6b243550-7cc1-4d75-8064-cef4c3d8be35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "061d42eb-6731-4a45-87ac-ed079aa31377", "AQAAAAEAACcQAAAAEF7YC92Er6qniTW/74lYLR5cQj4HYjVjNTRrWzyAg9s+QgLlhFO/hqiS+voSpP+nQA==", "e80ffaf1-d746-450f-be6d-49996be53cb8" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RecipeId",
                table: "AspNetUsers",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeUsers_RecipeId",
                table: "RecipeUsers",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Recipes_RecipeId",
                table: "AspNetUsers",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id");
        }
    }
}
