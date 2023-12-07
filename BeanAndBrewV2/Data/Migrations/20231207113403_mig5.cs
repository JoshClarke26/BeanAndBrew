using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeanAndBrewV2.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BakingLesson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Product = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BakingLesson", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BakingLessonSlot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tickets = table.Column<int>(type: "int", nullable: false),
                    BakingLessonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BakingLessonSlot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BakingLessonSlot_BakingLesson_BakingLessonId",
                        column: x => x.BakingLessonId,
                        principalTable: "BakingLesson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BakingLessonSlot_BakingLessonId",
                table: "BakingLessonSlot",
                column: "BakingLessonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BakingLessonSlot");

            migrationBuilder.DropTable(
                name: "BakingLesson");
        }
    }
}
