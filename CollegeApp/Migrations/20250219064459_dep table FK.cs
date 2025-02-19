using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI_Learning.Migrations
{
    /// <inheritdoc />
    public partial class deptableFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Student_Department",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "DepartmentName", "Description" },
                values: new object[,]
                {
                    { 1, "OT", "Operation Theater Technician" },
                    { 2, "School", "NA" },
                    { 3, "Haldirams", "Kapsi Plant" },
                    { 4, "GNM", "Bhandara College" },
                    { 5, "Singer", "Black Pink" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Address", "DOB", "DepartmentId", "Email", "StudentName" },
                values: new object[,]
                {
                    { 1, "Kapsi", new DateTime(2004, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "janvi@gmail.com", "Janvi" },
                    { 2, "Nagpur", new DateTime(2008, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "hasari@gmail.com", "Hasari" },
                    { 3, "Kapsi", new DateTime(2004, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "jay@gmail.com", "Jay" },
                    { 4, "Bhandara", new DateTime(2005, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "lisa@gmail.com", "Lisa" },
                    { 5, "Bhandara", new DateTime(2005, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "rosy@gmail.com", "Rosy" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_DepartmentId",
                table: "Students",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
