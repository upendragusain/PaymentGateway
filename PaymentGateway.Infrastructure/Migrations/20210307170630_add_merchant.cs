using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentGateway.Infrastructure.Migrations
{
    public partial class add_merchant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Charges_Merchant_MerchantId",
                table: "Charges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Merchant",
                table: "Merchant");

            migrationBuilder.RenameTable(
                name: "Merchant",
                newName: "Merchants");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Merchants",
                table: "Merchants",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Charges_Merchants_MerchantId",
                table: "Charges",
                column: "MerchantId",
                principalTable: "Merchants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Charges_Merchants_MerchantId",
                table: "Charges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Merchants",
                table: "Merchants");

            migrationBuilder.RenameTable(
                name: "Merchants",
                newName: "Merchant");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Merchant",
                table: "Merchant",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Charges_Merchant_MerchantId",
                table: "Charges",
                column: "MerchantId",
                principalTable: "Merchant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
