using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirPortService.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlyDate",
                table: "Reservations");

            migrationBuilder.AddColumn<bool>(
                name: "Confirmed",
                table: "Reservations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "FlyId",
                table: "Reservations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "SeatNumber",
                table: "Reservations",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confirmed",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "FlyId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "SeatNumber",
                table: "Reservations");

            migrationBuilder.AddColumn<DateTime>(
                name: "FlyDate",
                table: "Reservations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
