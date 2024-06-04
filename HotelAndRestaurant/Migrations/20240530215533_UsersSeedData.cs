using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAndRestaurant.Migrations
{
    public partial class UsersSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password", "RoleId" },
                values: new object[,]
                {
                    { 1, "admin@hotel.com", "admin", "", "hotel123", 1 },
                    { 2, "user@hotel.com", "user", "", "hotel123", 2 },
                    { 3, "receptionist@hotel.com", "receptionist", "", "hotel123", 3 },
                    { 4, "housekeeper1@hotel.com", "housekeeper", "", "hotel123", 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
