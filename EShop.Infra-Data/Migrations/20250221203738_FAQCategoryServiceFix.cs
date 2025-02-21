using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.Infra_Data.Migrations
{
    /// <inheritdoc />
    public partial class FAQCategoryServiceFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FAQs_FAQCategories_CategoryId",
                table: "FAQs");

            migrationBuilder.AddForeignKey(
                name: "FK_FAQs_FAQCategories_CategoryId",
                table: "FAQs",
                column: "CategoryId",
                principalTable: "FAQCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FAQs_FAQCategories_CategoryId",
                table: "FAQs");

            migrationBuilder.AddForeignKey(
                name: "FK_FAQs_FAQCategories_CategoryId",
                table: "FAQs",
                column: "CategoryId",
                principalTable: "FAQCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
