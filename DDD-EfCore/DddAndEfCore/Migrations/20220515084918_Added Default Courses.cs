using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DddAndEfCore.Migrations
{
    public partial class AddedDefaultCourses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Course",
                columns: new[] { "CourseID", "Name" },
                values: new object[] { new Guid("1b302e6f-81d6-4add-b8ef-424a4f685dd1"), "Calculus" });

            migrationBuilder.InsertData(
                table: "Course",
                columns: new[] { "CourseID", "Name" },
                values: new object[] { new Guid("5d77a202-5c7a-44a3-99f9-f3fe3d34a0d3"), "Chemistry" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "CourseID",
                keyValue: new Guid("1b302e6f-81d6-4add-b8ef-424a4f685dd1"));

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "CourseID",
                keyValue: new Guid("5d77a202-5c7a-44a3-99f9-f3fe3d34a0d3"));
        }
    }
}
