using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DddAndEfCore.Migrations
{
    public partial class SeeddataforSuffix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Suffix",
                columns: new[] { "SuffixID", "Name" },
                values: new object[] { new Guid("314687c5-576c-48c2-bd7b-db56f7b6a552"), "Jr" });

            migrationBuilder.InsertData(
                table: "Suffix",
                columns: new[] { "SuffixID", "Name" },
                values: new object[] { new Guid("bab15095-f4a3-4f36-a2b1-8e552e09407b"), "Sr" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Suffix",
                keyColumn: "SuffixID",
                keyValue: new Guid("314687c5-576c-48c2-bd7b-db56f7b6a552"));

            migrationBuilder.DeleteData(
                table: "Suffix",
                keyColumn: "SuffixID",
                keyValue: new Guid("bab15095-f4a3-4f36-a2b1-8e552e09407b"));
        }
    }
}
