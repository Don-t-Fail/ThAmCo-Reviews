using Microsoft.EntityFrameworkCore.Migrations;

namespace Reviews.Migrations
{
    public partial class UpdateTestAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "IsStaff" },
                values: new object[] { 42, false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "IsStaff" },
                values: new object[] { 7, false });
        }
    }
}