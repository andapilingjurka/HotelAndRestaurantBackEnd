using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAndRestaurant.Migrations
{
    public partial class RefreshMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "RoomType",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "RoomType",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "RoomType",
                keyColumn: "Id",
                keyValue: 23);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RoomType",
                columns: new[] { "Id", "RoomName" },
                values: new object[] { 21, "Single" });

            migrationBuilder.InsertData(
                table: "RoomType",
                columns: new[] { "Id", "RoomName" },
                values: new object[] { 22, "Double" });

            migrationBuilder.InsertData(
                table: "RoomType",
                columns: new[] { "Id", "RoomName" },
                values: new object[] { 23, "Suite" });

            migrationBuilder.InsertData(
                table: "Room",
                columns: new[] { "Id", "Description", "Image", "NeedsCleaning", "Price", "RoomNumber", "RoomTypeId", "Status" },
                values: new object[,]
                {
                    { 21, "A cozy single room.", "/images/image1.jpg", false, "100", 101, 21, 0 },
                    { 22, "A spacious double room.", "/images/image2.jpg", true, "150", 102, 22, 0 },
                    { 23, "A luxurious suite.", "/images/image3.jpg", false, "200", 103, 23, 0 },
                    { 24, "A comfortable twin room.", "/images/image4.jpg", false, "120", 104, 22, 0 },
                    { 25, "A deluxe double room.", "/images/image5.jpg", true, "180", 105, 22, 0 },
                    { 26, "A premium suite.", "/images/image6.jpg", false, "250", 106, 23, 0 }
                });
        }
    }
}
