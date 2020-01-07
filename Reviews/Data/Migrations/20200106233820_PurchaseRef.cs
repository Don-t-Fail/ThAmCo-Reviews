using Microsoft.EntityFrameworkCore.Migrations;

namespace Reviews.Migrations
{
    public partial class PurchaseRef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PurchaseRef",
                table: "Purchase",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseRef",
                table: "Purchase");
        }
    }
}
