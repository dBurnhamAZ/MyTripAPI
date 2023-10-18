using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyTripAPI.Migrations
{
    /// <inheritdoc />
    public partial class initialBuild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Suites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: false),
                    Sqft = table.Column<int>(type: "int", nullable: false),
                    Occupancy = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amenity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suites", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Suites",
                columns: new[] { "Id", "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2023, 10, 17, 20, 13, 41, 408, DateTimeKind.Local).AddTicks(1435), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", "C:\\Users\\nerme\\full-stack-learning-projects\\DotNetMastery_RESTAPIS\\ASP.NET MVC\\MagicSuite\\MagicSuite_SuiteAPI\\Images\\01.jpg", "Royal Suite", 5, 220.0, 550, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "", new DateTime(2023, 10, 17, 20, 13, 41, 408, DateTimeKind.Local).AddTicks(1447), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", "C:\\Users\\nerme\\full-stack-learning-projects\\DotNetMastery_RESTAPIS\\ASP.NET MVC\\MagicSuite\\MagicSuite_SuiteAPI\\Images\\01.jpg", "Ghoyal Suite", 4, 120.0, 230, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "", new DateTime(2023, 10, 17, 20, 13, 41, 408, DateTimeKind.Local).AddTicks(1449), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", "C:\\Users\\nerme\\full-stack-learning-projects\\DotNetMastery_RESTAPIS\\ASP.NET MVC\\MagicSuite\\MagicSuite_SuiteAPI\\Images\\01.jpg", "Shriya Ghoshal Suite", 3, 320.0, 140, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "", new DateTime(2023, 10, 17, 20, 13, 41, 408, DateTimeKind.Local).AddTicks(1451), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", "C:\\Users\\nerme\\full-stack-learning-projects\\DotNetMastery_RESTAPIS\\ASP.NET MVC\\MagicSuite\\MagicSuite_SuiteAPI\\Images\\01.jpg", "Social Suite", 2, 310.0, 400, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "", new DateTime(2023, 10, 17, 20, 13, 41, 408, DateTimeKind.Local).AddTicks(1453), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", "C:\\Users\\nerme\\full-stack-learning-projects\\DotNetMastery_RESTAPIS\\ASP.NET MVC\\MagicSuite\\MagicSuite_SuiteAPI\\Images\\01.jpg", "Ghost Suite", 1, 780.0, 480, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "", new DateTime(2023, 10, 17, 20, 13, 41, 408, DateTimeKind.Local).AddTicks(1454), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", "C:\\Users\\nerme\\full-stack-learning-projects\\DotNetMastery_RESTAPIS\\ASP.NET MVC\\MagicSuite\\MagicSuite_SuiteAPI\\Images\\01.jpg", "Koyal Ka Ghosla Suite", 6, 447.0, 700, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Suites");
        }
    }
}
