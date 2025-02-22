using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.Infra_Data.Migrations
{
    /// <inheritdoc />
    public partial class FAQCategoryServiceFix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Explanation",
                table: "FAQs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "FAQCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Explanation",
                table: "FAQs");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "FAQCategories");
        }
    }
}
