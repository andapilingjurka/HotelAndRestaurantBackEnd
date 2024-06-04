using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAndRestaurant.Migrations
{
    public partial class HashPassword_for_seedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$10$1R55Tn7R/fQV9emQEqknruao/IdfAWSUC8IqaYWAJLKHR2YPeHNCy");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$10$lEiAXZcTqba.Fz3Hl3nn2OXKWO8WuKOn5dbuQ23nDVI87QZADSS8q");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$10$ajnwQNvClDhjtja/704NcepnwbkIz4j6VgiRXojmcDWbSuVgZyz3C");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "$2a$10$Ah9zJ52Y8jlvWD.rtIzoCOEsaHssNzdvckMuELdt8wSCkRKxOkn16");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "hotel123");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "hotel123");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "hotel123");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "hotel123");
        }
    }
}
