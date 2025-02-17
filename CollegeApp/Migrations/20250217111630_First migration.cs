using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI_Learning.Migrations
{
    /// <inheritdoc />
    public partial class Firstmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SrNo = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

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
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "TestModels");
        }
    }
}
