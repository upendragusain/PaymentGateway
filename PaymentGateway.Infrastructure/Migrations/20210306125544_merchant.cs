using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentGateway.Infrastructure.Migrations
{
    public partial class merchant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FailureCode",
                table: "Charges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FailureMesage",
                table: "Charges",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MerchantId",
                table: "Charges",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentResponseId",
                table: "Charges",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Charges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Merchant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTimeUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchant", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Charges_MerchantId",
                table: "Charges",
                column: "MerchantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Charges_Merchant_MerchantId",
                table: "Charges",
                column: "MerchantId",
                principalTable: "Merchant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Charges_Merchant_MerchantId",
                table: "Charges");

            migrationBuilder.DropTable(
                name: "Merchant");

            migrationBuilder.DropIndex(
                name: "IX_Charges_MerchantId",
                table: "Charges");

            migrationBuilder.DropColumn(
                name: "FailureCode",
                table: "Charges");

            migrationBuilder.DropColumn(
                name: "FailureMesage",
                table: "Charges");

            migrationBuilder.DropColumn(
                name: "MerchantId",
                table: "Charges");

            migrationBuilder.DropColumn(
                name: "PaymentResponseId",
                table: "Charges");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Charges");
        }
    }
}
