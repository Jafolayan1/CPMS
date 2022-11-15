using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class updatechpTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Matric",
                table: "Chapters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "319b8124-4cb1-420d-a9d6-17f1cd61b48a", "AQAAAAEAACcQAAAAEMPlpwVodb5doPrP+J6wivHJvib6lWMZmgzp+lwjwHVbVm4T5Kx2yEqMHnmIdOa4JQ==", "86da0151-3bc3-483c-bfb2-c46021a1b9b8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d8dbbfcf-f4db-4a87-bab7-cee7c39f808c", "AQAAAAEAACcQAAAAEP8wejG2qvzhBsdGWe5OrynqBcBgsaPC3wO8C2Pm+GHQzazyM7Mnn2NfogJAZimcqg==", "f476395f-bcc8-4bef-a111-ed1ec6e4e68a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Matric",
                table: "Chapters");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1d1b65e2-be32-4726-b7d0-5c0a56eb0639", "AQAAAAEAACcQAAAAEIzLRBKz7vblzPQCrMKU1aOMZFfgUxJqU44QMIwzEB9UMqQGGcJqu9Gph/kD8CvE6A==", "bd0cde02-26bf-4233-8dde-9a048e4f8537" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0bca9f82-6dd2-4894-b7c2-1ae17e15682f", "AQAAAAEAACcQAAAAEFLr3/FhLVUnV52hzO9ihsZWM43thPbAPPTMZCHK1amghQ0YHOg9LLIE/EDv//T53Q==", "6f1447e1-2bee-4a0c-9b83-8c849314f66b" });
        }
    }
}
