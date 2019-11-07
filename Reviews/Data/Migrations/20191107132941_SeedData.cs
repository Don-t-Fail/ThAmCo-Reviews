using Microsoft.EntityFrameworkCore.Migrations;

namespace Reviews.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "IsStaff" },
                values: new object[] { 1, false });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "IsStaff" },
                values: new object[] { 2, true });

            migrationBuilder.InsertData(
                table: "Purchase",
                columns: new[] { "Id", "AccountId", "ProductId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "Purchase",
                columns: new[] { "Id", "AccountId", "ProductId" },
                values: new object[] { 2, 1, 2 });

            migrationBuilder.InsertData(
                table: "Review",
                columns: new[] { "Id", "Content", "IsVisible", "PurchaseId", "Rating" },
                values: new object[] { 1, "This is a test review for product 1", true, 1, 0 });

            migrationBuilder.InsertData(
                table: "Review",
                columns: new[] { "Id", "Content", "IsVisible", "PurchaseId", "Rating" },
                values: new object[] { 2, "This is a review for product 2", true, 2, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Review",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Review",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
