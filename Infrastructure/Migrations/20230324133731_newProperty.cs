using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class newProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProjectStudentId",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectStudentId",
                table: "Chapters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b1ae3942-1958-4777-8fc2-05a692ea43c8", "AQAAAAEAACcQAAAAEH9vuqjxSMfAueYxriNbDL1/CZMdyqW3GWl78rcbs6RZCNUhGZvvQcEZKUmjKiyBdQ==", "6dca5204-dcbe-4939-9dbc-5c934ed35d33" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectStudentId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectStudentId",
                table: "Chapters");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0e731cc6-ccf4-4dd1-ab1d-959f15d87afd", "AQAAAAEAACcQAAAAEIrNrvUlN1fKBIl+8RRjT72W5Dw3FZQduKbzNmay823nNOqCdQY5c6SsqYQr8ianPQ==", "0118c113-52cb-475e-ad1f-7bde253342cd" });
        }
    }
}
