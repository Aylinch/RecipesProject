using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipesProject.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Recipes",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Recipes");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6383851-4451-46c9-8d99-252c81e153ee",
                column: "ConcurrencyStamp",
                value: "2ba0921a-f640-410a-8a48-3b0bbae9a529");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "52ff7a8f-b2b1-4a92-9fa6-92785311d879",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e8759f06-dc7e-46aa-bcb2-5f42ab998ab3", "AQAAAAEAACcQAAAAENByCgiVvSWP8ftgFLjb+WkR8waMpv/rcppsvfF23ifYMo/6TrRYXyKDjxv5jH7DlQ==", "0cb340af-a7e8-4bab-9ce0-491e69282aad" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6b243550-7cc1-4d75-8064-cef4c3d8be35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "40e3c77c-8727-44cc-b198-795d2afb00ec", "AQAAAAEAACcQAAAAEHT+8ZC5vgkIbl+QKJrMFuhK36w/Ffb0Vg7JYxrXWxhSe/HPm9Bbs/h3sJ5oLessCA==", "25a957bc-fed0-483c-a9fe-26f9483ae77b" });
        }
    }
}
