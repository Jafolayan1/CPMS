using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class checks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "07353eef-d020-41b8-bc6f-85e5a44ffd95", "AQAAAAEAACcQAAAAEKbIEbR6sz1hRSqWWPUGX6xdNQWFT8Uk/MKaRmqhEkHg2rz4AjYHWDuwevZq38MT/g==", "04fec348-479e-444b-a2b5-4e1ab6a17f88" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6a73e9b8-fc3f-4cf7-8d2b-fbe87764dd16", "AQAAAAEAACcQAAAAEIoEpVe95ysltFDEeW5JlKSAVjYImhNpFv7sl6SS9q9Z7i6iua6h4ShHhXoWzZe2CA==", "9a41f349-01ee-4d65-ab1f-5c9a8e176c30" });
        }
    }
}
