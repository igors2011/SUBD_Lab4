using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SUBD.Migrations
{
    /// <inheritdoc />
    public partial class IndexMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductsInStore_Count",
                table: "ProductsInStore",
                column: "Count");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductsInStore_Count",
                table: "ProductsInStore");
        }
    }
}
