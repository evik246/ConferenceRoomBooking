using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConferenceRoomBooking.Dal.Db.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingService");

            migrationBuilder.DropTable(
                name: "ConferenceRoomService");

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

            migrationBuilder.CreateTable(
                name: "BookingEntityServiceEntity",
                columns: table => new
                {
                    BookingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServicesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingEntityServiceEntity", x => new { x.BookingsId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_BookingEntityServiceEntity_Bookings_BookingsId",
                        column: x => x.BookingsId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingEntityServiceEntity_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConferenceRoomEntityServiceEntity",
                columns: table => new
                {
                    ConferenceRoomsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServicesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConferenceRoomEntityServiceEntity", x => new { x.ConferenceRoomsId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_ConferenceRoomEntityServiceEntity_ConferenceRooms_ConferenceRoomsId",
                        column: x => x.ConferenceRoomsId,
                        principalTable: "ConferenceRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConferenceRoomEntityServiceEntity_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ConferenceRooms",
                columns: new[] { "Id", "Capacity", "DateCreated", "LastModifiedDate", "Name", "PricePerHour" },
                values: new object[,]
                {
                    { new Guid("077eca9b-7b0c-4edb-86ca-12dab06958b5"), 30, new DateTime(2024, 10, 25, 13, 36, 51, 898, DateTimeKind.Local).AddTicks(1173), new DateTime(2024, 10, 25, 13, 36, 51, 898, DateTimeKind.Local).AddTicks(1174), "C", 1500m },
                    { new Guid("62fe6ee7-0497-46d6-a51d-96fdf673fc48"), 100, new DateTime(2024, 10, 25, 13, 36, 51, 898, DateTimeKind.Local).AddTicks(1169), new DateTime(2024, 10, 25, 13, 36, 51, 898, DateTimeKind.Local).AddTicks(1170), "B", 3500m },
                    { new Guid("f16c6f1e-4fe5-45ee-843d-bb0385f872be"), 50, new DateTime(2024, 10, 25, 13, 36, 51, 898, DateTimeKind.Local).AddTicks(1119), new DateTime(2024, 10, 25, 13, 36, 51, 898, DateTimeKind.Local).AddTicks(1165), "A", 2000m }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "DateCreated", "LastModifiedDate", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("09d4c617-121a-4f26-b16b-35b0a7067e7e"), new DateTime(2024, 10, 25, 13, 36, 51, 898, DateTimeKind.Local).AddTicks(1865), new DateTime(2024, 10, 25, 13, 36, 51, 898, DateTimeKind.Local).AddTicks(1866), "Wi-Fi", 300m },
                    { new Guid("ab15467d-7b56-4c3d-a3ff-8078ff1b65e9"), new DateTime(2024, 10, 25, 13, 36, 51, 898, DateTimeKind.Local).AddTicks(1858), new DateTime(2024, 10, 25, 13, 36, 51, 898, DateTimeKind.Local).AddTicks(1862), "Проєктор", 500m },
                    { new Guid("b198648c-2307-4223-8b4d-7758f88181c4"), new DateTime(2024, 10, 25, 13, 36, 51, 898, DateTimeKind.Local).AddTicks(1946), new DateTime(2024, 10, 25, 13, 36, 51, 898, DateTimeKind.Local).AddTicks(1948), "Звук", 700m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingEntityServiceEntity_ServicesId",
                table: "BookingEntityServiceEntity",
                column: "ServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_ConferenceRoomEntityServiceEntity_ServicesId",
                table: "ConferenceRoomEntityServiceEntity",
                column: "ServicesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingEntityServiceEntity");

            migrationBuilder.DropTable(
                name: "ConferenceRoomEntityServiceEntity");

            migrationBuilder.DeleteData(
                table: "ConferenceRooms",
                keyColumn: "Id",
                keyValue: new Guid("077eca9b-7b0c-4edb-86ca-12dab06958b5"));

            migrationBuilder.DeleteData(
                table: "ConferenceRooms",
                keyColumn: "Id",
                keyValue: new Guid("62fe6ee7-0497-46d6-a51d-96fdf673fc48"));

            migrationBuilder.DeleteData(
                table: "ConferenceRooms",
                keyColumn: "Id",
                keyValue: new Guid("f16c6f1e-4fe5-45ee-843d-bb0385f872be"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("09d4c617-121a-4f26-b16b-35b0a7067e7e"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("ab15467d-7b56-4c3d-a3ff-8078ff1b65e9"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("b198648c-2307-4223-8b4d-7758f88181c4"));

            migrationBuilder.CreateTable(
                name: "BookingService",
                columns: table => new
                {
                    BookingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServicesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingService", x => new { x.BookingsId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_BookingService_Bookings_BookingsId",
                        column: x => x.BookingsId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingService_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConferenceRoomService",
                columns: table => new
                {
                    ConferenceRoomsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServicesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConferenceRoomService", x => new { x.ConferenceRoomsId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_ConferenceRoomService_ConferenceRooms_ConferenceRoomsId",
                        column: x => x.ConferenceRoomsId,
                        principalTable: "ConferenceRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConferenceRoomService_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_BookingService_ServicesId",
                table: "BookingService",
                column: "ServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_ConferenceRoomService_ServicesId",
                table: "ConferenceRoomService",
                column: "ServicesId");
        }
    }
}
