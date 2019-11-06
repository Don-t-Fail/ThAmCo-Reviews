using Microsoft.EntityFrameworkCore.Migrations;

namespace Reviews.Migrations
{
    public partial class TweakReviewModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Review_PurchaseId",
                table: "Review",
                column: "PurchaseId");

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
        }
    }
}
