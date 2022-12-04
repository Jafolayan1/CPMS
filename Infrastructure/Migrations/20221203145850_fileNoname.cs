using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class fileNoname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmployeeNo",
                table: "Supervisors",
                newName: "FileNo");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a536e698-85ee-4710-ad57-e7d856ee7f82", "AQAAAAEAACcQAAAAEDCGPggvVe1VufpZnzG5Tjxhez1xucDEdIQ8u7ix6Jh3hv7w53ILSzDxk37F9Q2POA==", "26c96dcb-1495-4073-b3f4-e57cd48b2103" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "01f93983-5059-4ae3-9185-170e4195563c", "AQAAAAEAACcQAAAAEI+g5YGh6CAQX1TQR7vkc6wuqScebor00QiVfnwngS790bUOocR+WU0w7ntVeslpKA==", "56697577-680e-4a69-bfa5-4de54b19f6ea" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileNo",
                table: "Supervisors",
                newName: "EmployeeNo");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fece5060-703d-4a39-9fb1-ff5929461c99", "AQAAAAEAACcQAAAAEApihHczwagUe0h+enDajyhFrnWmvB3X3mMRkuWxaTeFA07dFUWhgKc/+Q1P+ejDbA==", "22fce5ac-2068-4514-8013-2fea69221cd8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b92876da-6e7f-4f28-91e1-633f33c45e25", "AQAAAAEAACcQAAAAENalXisI9uxAD+QNhUQ19yfGhTzox7PlPkZXYJ8AHV/a62ZwxMYABhiFjyaeM/iwhA==", "f7e10412-2051-4d70-b53f-4207baf6d937" });
        }
    }
}
