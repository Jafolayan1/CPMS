using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class tblSupervisor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "84e95ab8-567d-4d50-8812-2cda0a5d2b16", "AQAAAAEAACcQAAAAEPjarVXCw3MV4zvNAEql85mf9Le6CgMSYMhXwfxnn54xq3XuxIhyZKwf4+P9XbHaMg==", "aec658f4-737d-43cb-ae55-bd0573922c6e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ec7124bf-0e2f-41b3-be3b-d72f472da64f", "AQAAAAEAACcQAAAAEAY5IFvK9Tz4xgN1kO2+iEmJ7DHhjJ7KOmjSx47LGRit8eVt6KoiNerDpPwuMrt7Bw==", "ecec12c8-9292-4bd7-a5d6-c4954a5215b6" });
        }
    }
}
