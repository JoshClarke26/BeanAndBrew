using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeanAndBrewV2.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "HamperItem",
                columns: new[] { "Name", "Price", "ImagePath" },
                values: new object[] { "All Day Breakfast Wrap", 3.50, "Ult_Breakfast_Wrap_Thumb.jpg" });
            migrationBuilder.InsertData(
                table: "HamperItem",
                columns: new[] { "Name", "Price", "ImagePath" },
                values: new object[] { "Bacon Bap", 2.80, "British_Bacon_Bap_Thumb.jpg" });
            migrationBuilder.InsertData(
                table: "HamperItem",
                columns: new[] { "Name", "Price", "ImagePath" },
                values: new object[] { "Suasage Bap", 2.80, "Pork_Sausage_Bap_Thumb.jpg" });
            migrationBuilder.InsertData(
                table: "HamperItem",
                columns: new[] { "Name", "Price", "ImagePath" },
                values: new object[] { "Croissant", 2.50, "New_All_Butter_Crossiant_Thumb.jpg" });
            migrationBuilder.InsertData(
                table: "HamperItem",
                columns: new[] { "Name", "Price", "ImagePath" },
                values: new object[] { "Toast", 1.95, "Value_Offer_White_Toast_Thumb.jpg" });
            migrationBuilder.InsertData(
                table: "HamperItem",
                columns: new[] { "Name", "Price", "ImagePath" },
                values: new object[] { "Orange Cake", 3.00, "Orange_Cake.png" });
            migrationBuilder.InsertData(
                table: "HamperItem",
                columns: new[] { "Name", "Price", "ImagePath" },
                values: new object[] { "Christmas Shortcake", 2.60, "Christmas_Tree_Shortcake_Thumb.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
