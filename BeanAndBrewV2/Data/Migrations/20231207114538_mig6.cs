using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeanAndBrewV2.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BakingLessonSlot",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BakingLessonSlot_UserId",
                table: "BakingLessonSlot",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BakingLessonSlot_AspNetUsers_UserId",
                table: "BakingLessonSlot",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BakingLessonSlot_AspNetUsers_UserId",
                table: "BakingLessonSlot");

            migrationBuilder.DropIndex(
                name: "IX_BakingLessonSlot_UserId",
                table: "BakingLessonSlot");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BakingLessonSlot");
        }
    }
}
