using Microsoft.EntityFrameworkCore.Migrations;

namespace Reviews.Migrations
{
    public partial class AdditionalSeedDataTesting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "IsStaff" },
                values: new object[] { 6, false });

            migrationBuilder.InsertData(
                table: "Purchase",
                columns: new[] { "Id", "AccountId", "ProductId" },
                values: new object[] { 7, 6, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
