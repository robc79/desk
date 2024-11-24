using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Sql.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUniqueTagName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tags_Name",
                table: "Tags");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tags_Name",
                table: "Tags",
                column: "Name",
                unique: true);
        }
    }
}
