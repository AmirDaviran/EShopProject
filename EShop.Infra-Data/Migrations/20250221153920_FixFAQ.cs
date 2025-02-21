using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.Infra_Data.Migrations
{
    /// <inheritdoc />
    public partial class FixFAQ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Explenation",
                table: "FAQs",
                newName: "Explanation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Explanation",
                table: "FAQs",
                newName: "Explenation");
        }
    }
}
