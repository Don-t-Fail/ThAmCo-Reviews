using Microsoft.EntityFrameworkCore.Migrations;

namespace Reviews.Migrations
{
    public partial class PurchaseRef2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PurchaseRef",
                table: "Review",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseRef",
                table: "Review");
        }
    }
}
