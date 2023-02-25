using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class chnagedsomeprojectpropertfromListtoICollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6a73e9b8-fc3f-4cf7-8d2b-fbe87764dd16", "AQAAAAEAACcQAAAAEIoEpVe95ysltFDEeW5JlKSAVjYImhNpFv7sl6SS9q9Z7i6iua6h4ShHhXoWzZe2CA==", "9a41f349-01ee-4d65-ab1f-5c9a8e176c30" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d9e51f11-2ef8-4deb-a073-d3bd552670c4", "AQAAAAEAACcQAAAAEPlRTx+Yo3lnkd/hcAo14hIbXmNki6iljIzPI/qXfeRBR3GYZ570V3U6oUt2pRA+yQ==", "ed51289e-32a5-43c3-8e71-a3fa35f49f76" });
        }
    }
}
