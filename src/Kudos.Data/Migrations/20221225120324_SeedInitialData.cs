using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Kudos.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "Kudo",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, "Name", "Surname" },
                    { 2, "Name", "Surname" }
                });

            migrationBuilder.InsertData(
                table: "Kudo",
                columns: new[] { "Id", "DateCreated", "Description", "Exchanged", "Reason", "ReceiverId", "SenderId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 12, 25, 13, 3, 24, 788, DateTimeKind.Local).AddTicks(5937), "text, text, text, text, text, text, text, text", false, "Team Player", 2, 1 },
                    { 2, new DateTime(2022, 12, 25, 13, 3, 24, 788, DateTimeKind.Local).AddTicks(5991), "text, text, text, text, text, text, text, text", false, "Ownership Mindset", 1, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Kudo",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Kudo",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "Kudo",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
