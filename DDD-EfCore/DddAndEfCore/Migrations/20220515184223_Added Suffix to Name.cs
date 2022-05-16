using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DddAndEfCore.Migrations
{
    public partial class AddedSuffixtoName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NameSuffixID",
                table: "Student",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Suffix",
                columns: table => new
                {
                    SuffixID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suffix", x => x.SuffixID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Student_NameSuffixID",
                table: "Student",
                column: "NameSuffixID");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Suffix_NameSuffixID",
                table: "Student",
                column: "NameSuffixID",
                principalTable: "Suffix",
                principalColumn: "SuffixID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Suffix_NameSuffixID",
                table: "Student");

            migrationBuilder.DropTable(
                name: "Suffix");

            migrationBuilder.DropIndex(
                name: "IX_Student_NameSuffixID",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "NameSuffixID",
                table: "Student");
        }
    }
}
