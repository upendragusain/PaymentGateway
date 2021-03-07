using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentGateway.Infrastructure.Migrations
{
    public partial class merchant_hack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Charges",
                table: "Charges");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Charges",
                table: "Charges",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Charges_MerchantId",
                table: "Charges",
                column: "MerchantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Charges",
                table: "Charges");

            migrationBuilder.DropIndex(
                name: "IX_Charges_MerchantId",
                table: "Charges");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Charges",
                table: "Charges",
                columns: new[] { "MerchantId", "Id" });
        }
    }
}
