using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Laptopy_APIs.Migrations
{
    /// <inheritdoc />
    public partial class add_model_column_to_laptops : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Laptops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Model",
                table: "Laptops");
        }
    }
}
