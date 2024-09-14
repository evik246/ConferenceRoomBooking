using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConferenceRoomBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ConferenceRooms",
                columns: new[] { "Id", "Capacity", "DateCreated", "LastModifiedDate", "Name", "PricePerHour" },
                values: new object[,]
                {
                    { new Guid("5bff1fc7-c763-4016-bf84-c37e8a3963f1"), 30, new DateTime(2024, 9, 14, 4, 19, 52, 374, DateTimeKind.Local).AddTicks(3089), new DateTime(2024, 9, 14, 4, 19, 52, 374, DateTimeKind.Local).AddTicks(3090), "C", 1500m },
                    { new Guid("725dc2f9-b606-4d3e-a9cf-c3e5334cc901"), 50, new DateTime(2024, 9, 14, 4, 19, 52, 374, DateTimeKind.Local).AddTicks(3034), new DateTime(2024, 9, 14, 4, 19, 52, 374, DateTimeKind.Local).AddTicks(3084), "A", 2000m },
                    { new Guid("801dfec5-ec5c-43f2-bfca-a8202faf3fb9"), 100, new DateTime(2024, 9, 14, 4, 19, 52, 374, DateTimeKind.Local).AddTicks(3087), new DateTime(2024, 9, 14, 4, 19, 52, 374, DateTimeKind.Local).AddTicks(3088), "B", 3500m }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "DateCreated", "LastModifiedDate", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("2ae51f1f-16db-463d-a3c6-9bdb186edfe7"), new DateTime(2024, 9, 14, 4, 19, 52, 374, DateTimeKind.Local).AddTicks(3830), new DateTime(2024, 9, 14, 4, 19, 52, 374, DateTimeKind.Local).AddTicks(3839), "Проєктор", 500m },
                    { new Guid("5f5c3b94-e920-43cd-8380-72a83924e177"), new DateTime(2024, 9, 14, 4, 19, 52, 374, DateTimeKind.Local).AddTicks(3841), new DateTime(2024, 9, 14, 4, 19, 52, 374, DateTimeKind.Local).AddTicks(3842), "Wi-Fi", 300m },
                    { new Guid("b2bda178-dc09-4c91-a18b-a9c704e11578"), new DateTime(2024, 9, 14, 4, 19, 52, 374, DateTimeKind.Local).AddTicks(3844), new DateTime(2024, 9, 14, 4, 19, 52, 374, DateTimeKind.Local).AddTicks(3845), "Звук", 700m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ConferenceRooms",
                keyColumn: "Id",
                keyValue: new Guid("5bff1fc7-c763-4016-bf84-c37e8a3963f1"));

            migrationBuilder.DeleteData(
                table: "ConferenceRooms",
                keyColumn: "Id",
                keyValue: new Guid("725dc2f9-b606-4d3e-a9cf-c3e5334cc901"));

            migrationBuilder.DeleteData(
                table: "ConferenceRooms",
                keyColumn: "Id",
                keyValue: new Guid("801dfec5-ec5c-43f2-bfca-a8202faf3fb9"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("2ae51f1f-16db-463d-a3c6-9bdb186edfe7"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("5f5c3b94-e920-43cd-8380-72a83924e177"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("b2bda178-dc09-4c91-a18b-a9c704e11578"));
        }
    }
}
