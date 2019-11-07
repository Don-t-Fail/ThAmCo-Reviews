using Microsoft.EntityFrameworkCore.Migrations;

namespace Reviews.Migrations
{
    public partial class SwitchPurchaseLinkTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Account_AccountId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_AccountId",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Review");

            migrationBuilder.CreateIndex(
                name: "IX_Review_PurchaseId",
                table: "Review",
                column: "PurchaseId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Purchase_PurchaseId",
                table: "Review",
                column: "PurchaseId",
                principalTable: "Purchase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Purchase_PurchaseId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_PurchaseId",
                table: "Review");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Review",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Review",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Review_AccountId",
                table: "Review",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Account_AccountId",
                table: "Review",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
