using Microsoft.EntityFrameworkCore.Migrations;

namespace Reviews.Migrations
{
    public partial class AdditionalTestData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "IsStaff" },
                values: new object[,]
                {
                    { 4, false },
                    { 5, false }
                });

            migrationBuilder.InsertData(
                table: "Purchase",
                columns: new[] { "Id", "AccountId", "ProductId" },
                values: new object[] { 5, 3, 3 });

            migrationBuilder.UpdateData(
                table: "Review",
                keyColumn: "Id",
                keyValue: 3,
                column: "Rating",
                value: 3);

            migrationBuilder.InsertData(
                table: "Purchase",
                columns: new[] { "Id", "AccountId", "ProductId" },
                values: new object[] { 4, 4, 1 });

            migrationBuilder.InsertData(
                table: "Purchase",
                columns: new[] { "Id", "AccountId", "ProductId" },
                values: new object[] { 6, 5, 1 });

            migrationBuilder.InsertData(
                table: "Review",
                columns: new[] { "Id", "Content", "IsVisible", "PurchaseId", "Rating" },
                values: new object[] { 5, "This is a test review for product 3", true, 5, 3 });

            migrationBuilder.InsertData(
                table: "Review",
                columns: new[] { "Id", "Content", "IsVisible", "PurchaseId", "Rating" },
                values: new object[] { 4, "This is a test review for product 1", false, 4, 1 });

            migrationBuilder.InsertData(
                table: "Review",
                columns: new[] { "Id", "Content", "IsVisible", "PurchaseId", "Rating" },
                values: new object[] { 6, "This is a test review for product 1", true, 6, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Review",
                keyColumn: "Id",
                keyValue: 3,
                column: "Rating",
                value: 5);
        }
    }
}
