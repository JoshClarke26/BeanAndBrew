using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeanAndBrewV2.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hamper",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hamper", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hamper_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HamperItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HamperItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HamperItemOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    HamperItemId = table.Column<int>(type: "int", nullable: false),
                    HamperId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HamperItemOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HamperItemOrder_HamperItem_HamperItemId",
                        column: x => x.HamperItemId,
                        principalTable: "HamperItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HamperItemOrder_Hamper_HamperId",
                        column: x => x.HamperId,
                        principalTable: "Hamper",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hamper_UserId",
                table: "Hamper",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HamperItemOrder_HamperId",
                table: "HamperItemOrder",
                column: "HamperId");

            migrationBuilder.CreateIndex(
                name: "IX_HamperItemOrder_HamperItemId",
                table: "HamperItemOrder",
                column: "HamperItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HamperItemOrder");

            migrationBuilder.DropTable(
                name: "HamperItem");

            migrationBuilder.DropTable(
                name: "Hamper");
        }
    }
}
