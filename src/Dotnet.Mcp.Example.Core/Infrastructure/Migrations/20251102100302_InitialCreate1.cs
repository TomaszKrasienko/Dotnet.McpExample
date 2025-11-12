using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dotnet.Mcp.Example.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Contractor_ContractorId",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_ContractorId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "OrderPositions");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "InvoicePosition",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "InvoicePosition");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "OrderPositions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_ContractorId",
                table: "Invoice",
                column: "ContractorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Contractor_ContractorId",
                table: "Invoice",
                column: "ContractorId",
                principalTable: "Contractor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
