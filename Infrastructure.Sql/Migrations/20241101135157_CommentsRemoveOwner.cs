using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Sql.Migrations
{
    /// <inheritdoc />
    public partial class CommentsRemoveOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TextComments_AspNetUsers_OwnerId",
                table: "TextComments");

            migrationBuilder.DropIndex(
                name: "IX_TextComments_OwnerId",
                table: "TextComments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TextComments_OwnerId",
                table: "TextComments",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TextComments_AspNetUsers_OwnerId",
                table: "TextComments",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
