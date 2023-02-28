using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class addedvirtualtosomeprojectproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d9e51f11-2ef8-4deb-a073-d3bd552670c4", "AQAAAAEAACcQAAAAEPlRTx+Yo3lnkd/hcAo14hIbXmNki6iljIzPI/qXfeRBR3GYZ570V3U6oUt2pRA+yQ==", "ed51289e-32a5-43c3-8e71-a3fa35f49f76" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "551ec069-f312-429c-bae2-ef80e15603e8", "AQAAAAEAACcQAAAAEF9ayxTbiiyL9yPKFBE8OlFD8/GzU6PgYrwzQQBf6dFLFP1XkLFTOAYEdpn+Z8zlrg==", "88ec7834-0d86-4934-a8d9-26e50918d405" });
        }
    }
}
