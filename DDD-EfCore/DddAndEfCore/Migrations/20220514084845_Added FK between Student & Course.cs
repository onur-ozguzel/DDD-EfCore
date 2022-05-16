using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DddAndEfCore.Migrations
{
    public partial class AddedFKbetweenStudentCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "FavoriteCourseId",
                table: "Student",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Student_FavoriteCourseId",
                table: "Student",
                column: "FavoriteCourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Course_FavoriteCourseId",
                table: "Student",
                column: "FavoriteCourseId",
                principalTable: "Course",
                principalColumn: "CourseID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Course_FavoriteCourseId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_FavoriteCourseId",
                table: "Student");

            migrationBuilder.AlterColumn<Guid>(
                name: "FavoriteCourseId",
                table: "Student",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
