using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAndRestaurant.Migrations
{
    public partial class SeedData_for_roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Administrator with full access to all features and settings.", "admin" },
                    { 2, "Regular user with access to basic features.", "user" },
                    { 3, "Receptionist with access to front desk functionalities.", "receptionist" },
                    { 4, "Housekeeper with access to housekeeping-related features.", "housekeeper" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
