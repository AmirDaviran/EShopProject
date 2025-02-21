using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.Infra_Data.Migrations
{
    /// <inheritdoc />
    public partial class FAQCategoryService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Explanation",
                table: "FAQs");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "FAQs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FAQCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FAQs_CategoryId",
                table: "FAQs",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_FAQs_FAQCategories_CategoryId",
                table: "FAQs",
                column: "CategoryId",
                principalTable: "FAQCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FAQs_FAQCategories_CategoryId",
                table: "FAQs");

            migrationBuilder.DropTable(
                name: "FAQCategories");

            migrationBuilder.DropIndex(
                name: "IX_FAQs_CategoryId",
                table: "FAQs");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "FAQs");

            migrationBuilder.AddColumn<string>(
                name: "Explanation",
                table: "FAQs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
