using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class refactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6373d128-d4e8-408e-8fbe-56aa1136f8d3", "AQAAAAEAACcQAAAAEHOuCEN2z7JxyqzyAt65jsmhWwifvzbXMsOmVj0RHFG7czWhghqmnH7XfZfIjgJaBA==", "4224a506-316d-43b3-b656-c1985f877a67" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b1ae3942-1958-4777-8fc2-05a692ea43c8", "AQAAAAEAACcQAAAAEH9vuqjxSMfAueYxriNbDL1/CZMdyqW3GWl78rcbs6RZCNUhGZvvQcEZKUmjKiyBdQ==", "6dca5204-dcbe-4939-9dbc-5c934ed35d33" });
        }
    }
}
