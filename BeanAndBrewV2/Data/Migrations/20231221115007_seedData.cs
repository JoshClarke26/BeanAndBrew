using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeanAndBrewV2.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coffee",
                columns: new[] { "Name", "Price", "Size", "ImagePath" },
                values: new object[] { "Espresso", 3.20, 300, "03236d8e-a387-4f4f-8e43-65a922021705.png" });

            migrationBuilder.InsertData(
                table: "Coffee",
                columns: new[] { "Name", "Price", "Size", "ImagePath" },
                values: new object[] { "Cappuccino", 3.50, 300, "07ef0b54-1d9c-4402-9222-820b3a6d5fee.png" });

            migrationBuilder.InsertData(
                table: "Coffee",
                columns: new[] { "Name", "Price", "Size", "ImagePath" },
                values: new object[] { "Mocha", 3.50, 300, "56c4eb85-3a51-4bd4-9227-dd777e31699b.png" });

            migrationBuilder.InsertData(
                table: "Coffee",
                columns: new[] { "Name", "Price", "Size", "ImagePath" },
                values: new object[] { "Latte", 3.50, 420, "6c7f263a-6c93-427d-9b93-1f8f78ebb55e.png" });

            migrationBuilder.InsertData(
                table: "Coffee",
                columns: new[] { "Name", "Price", "Size", "ImagePath" },
                values: new object[] { "Americano", 3.50, 330, "b4433f21-0d10-4ef1-bfda-59541ba43bcd.png" });

            migrationBuilder.InsertData(
                table: "Coffee",
                columns: new[] { "Name", "Price", "Size", "ImagePath" },
                values: new object[] { "Flat White", 3.50, 330, "c7049eff-e0c1-4f86-a625-b2ba309c26cb.png" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
