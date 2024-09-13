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
                    { new Guid("5a9f9296-10f1-4716-9124-c44952a4b96c"), 100, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B", 3500m },
                    { new Guid("5fcc5da0-eeeb-44cf-8e09-9f2969c26456"), 50, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A", 2000m },
                    { new Guid("d9aad8f7-ad86-4979-b47b-63afcca8c55e"), 30, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "C", 1500m }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "DateCreated", "LastModifiedDate", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("28281559-6af0-4e08-b477-1d60fab8ae9c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Звук", 700m },
                    { new Guid("8b6e44e4-9f01-4818-b9cb-3a3360ceaa7f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wi-Fi", 300m },
                    { new Guid("a8c4d30f-7469-48cd-96e0-dd66d121aebe"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Проєктор", 500m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ConferenceRooms",
                keyColumn: "Id",
                keyValue: new Guid("5a9f9296-10f1-4716-9124-c44952a4b96c"));

            migrationBuilder.DeleteData(
                table: "ConferenceRooms",
                keyColumn: "Id",
                keyValue: new Guid("5fcc5da0-eeeb-44cf-8e09-9f2969c26456"));

            migrationBuilder.DeleteData(
                table: "ConferenceRooms",
                keyColumn: "Id",
                keyValue: new Guid("d9aad8f7-ad86-4979-b47b-63afcca8c55e"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("28281559-6af0-4e08-b477-1d60fab8ae9c"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("8b6e44e4-9f01-4818-b9cb-3a3360ceaa7f"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("a8c4d30f-7469-48cd-96e0-dd66d121aebe"));
        }
    }
}
