using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAndRestaurant.Migrations
{
    public partial class Fixed_RoomNotificationRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Room_RoomId",
                table: "Notifications");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Room_RoomId",
                table: "Notifications",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Room_RoomId",
                table: "Notifications");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Room_RoomId",
                table: "Notifications",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
