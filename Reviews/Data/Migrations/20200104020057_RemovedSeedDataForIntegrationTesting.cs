using Microsoft.EntityFrameworkCore.Migrations;

namespace Reviews.Migrations
{
    public partial class RemovedSeedDataForIntegrationTesting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Review",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Review",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Review",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Review",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Review",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Review",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 6);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Purchase",
                columns: new[] { "Id", "AccountId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 3, 1 },
                    { 4, 4, 1 },
                    { 5, 3, 3 },
                    { 6, 5, 1 },
                    { 7, 6, 1 }
                });

            migrationBuilder.InsertData(
                table: "Review",
                columns: new[] { "Id", "Content", "IsVisible", "PurchaseId", "Rating" },
                values: new object[,]
                {
                    { 1, "This is a test review for product 1", true, 1, 5 },
                    { 2, "This is a review for product 2", true, 2, 2 },
                    { 3, "This is a test review for product 1", true, 3, 3 },
                    { 4, "This is a test review for product 1", false, 4, 1 },
                    { 5, "This is a test review for product 3", true, 5, 3 },
                    { 6, "This is a test review for product 1", true, 6, 3 }
                });
        }
    }
}