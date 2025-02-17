using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI_Learning.Migrations
{
    /// <inheritdoc />
    public partial class withdataMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Address", "DOB", "Email", "StudentName" },
                values: new object[,]
                {
                    { 1, "Kapsi", new DateTime(2004, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "janvi@gmail.com", "Janvi" },
                    { 2, "Nagpur", new DateTime(2008, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "hasari@gmail.com", "Hasari" },
                    { 3, "Kapsi", new DateTime(2004, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "jay@gmail.com", "Jay" },
                    { 4, "Bhandara", new DateTime(2005, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "lisa@gmail.com", "Lisa" },
                    { 5, "Bhandara", new DateTime(2005, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "rosy@gmail.com", "Rosy" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
