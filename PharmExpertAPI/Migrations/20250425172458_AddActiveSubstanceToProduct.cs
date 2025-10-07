using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmExpertAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddActiveSubstanceToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActiveSubstance",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveSubstance",
                table: "Products");
        }
    }
}
