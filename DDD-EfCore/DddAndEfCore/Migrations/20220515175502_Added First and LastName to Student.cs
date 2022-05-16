using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DddAndEfCore.Migrations
{
    public partial class AddedFirstandLastNametoStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Student",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Student");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Student",
                newName: "Name");
        }
    }
}
