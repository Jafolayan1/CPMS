using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class complaintTBL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Complaints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    When = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complaints", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "08436e24-dcff-466a-b552-1350e3cfceae", "AQAAAAEAACcQAAAAEH1XRknzLk1K62Oyl9FjC9vNwJ6dqPmyj2f21OOFqz4B+EVnhU8MwqFwV//fv5fVTA==", "2b55e1ea-2de0-4d06-8e29-50b4de7993bb" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Complaints");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6373d128-d4e8-408e-8fbe-56aa1136f8d3", "AQAAAAEAACcQAAAAEHOuCEN2z7JxyqzyAt65jsmhWwifvzbXMsOmVj0RHFG7czWhghqmnH7XfZfIjgJaBA==", "4224a506-316d-43b3-b656-c1985f877a67" });
        }
    }
}
