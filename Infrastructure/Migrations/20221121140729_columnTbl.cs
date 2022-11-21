using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class columnTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Supervisors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8240a964-f539-4cae-8c13-3e2c2a3cb2ec", "AQAAAAEAACcQAAAAEH+pf6qMUQSWzNJVGmkDala9KFqGtMMixyb/7MO8jDo2b97TRwiO9R+ejjiDk2ZBXA==", "ccbfe7f2-1fde-4cc7-acde-401af04468ec" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "37bfaad7-a3a7-4e40-a048-17194aead730", "AQAAAAEAACcQAAAAEGy7uCbk0LtGdDNHlPte6ThyidqOwIONO5UvqA1X6yhMJJvWCzg5BztIjSRBTqdBeQ==", "600e8d74-9dfd-49e0-97d0-435b6fdda852" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Supervisors");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Students");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bcd86492-bf94-46c8-a7e6-c66824f39e0d", "AQAAAAEAACcQAAAAEJEfK8TRlRQhavsngBNn2k0e5l+iyH3t2Tb7hXk+NRQaMYlraJHXZv5RUL6o5TOSqQ==", "45a6680f-2a52-4b85-ba7b-90c3dc9f465c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a3e693ff-fb82-4c26-947a-6353f05a53cc", "AQAAAAEAACcQAAAAEEgjBEVOD2uyGtJ44W5ESmmkFx56CdOksAi08/9q17IsPhAlkQTDnWjUsyLLcDfNiQ==", "37369ab4-f211-428e-b498-4109e3a06f6b" });
        }
    }
}