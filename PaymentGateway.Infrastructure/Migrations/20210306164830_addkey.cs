using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentGateway.Infrastructure.Migrations
{
    public partial class addkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Charges_Id",
                table: "Cards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Charges",
                table: "Charges");

            migrationBuilder.DropIndex(
                name: "IX_Charges_MerchantId",
                table: "Charges");

            migrationBuilder.AddColumn<Guid>(
                name: "MerchantId",
                table: "Cards",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Charges",
                table: "Charges",
                columns: new[] { "MerchantId", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_MerchantId_Id",
                table: "Cards",
                columns: new[] { "MerchantId", "Id" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Charges_MerchantId_Id",
                table: "Cards",
                columns: new[] { "MerchantId", "Id" },
                principalTable: "Charges",
                principalColumns: new[] { "MerchantId", "Id" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Charges_MerchantId_Id",
                table: "Cards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Charges",
                table: "Charges");

            migrationBuilder.DropIndex(
                name: "IX_Cards_MerchantId_Id",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "MerchantId",
                table: "Cards");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Charges",
                table: "Charges",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Charges_MerchantId",
                table: "Charges",
                column: "MerchantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Charges_Id",
                table: "Cards",
                column: "Id",
                principalTable: "Charges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
