using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dotnet.Mcp.Example.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderPosition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "OrderPositions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "OrderPositions");
        }
    }
}
