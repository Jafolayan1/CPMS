using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class emailnullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "184ab5c2-1617-44fd-80ce-ffaa7e5dd72e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8544235a-7fe5-4e40-90f6-8d4666d4077b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b3d5bafa-ba47-4efd-8513-3ed6fc15032b");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Supervisors",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "335a2c1a-f76e-49f1-b29c-ea54721c0c50", "00000000-0000-0000-0000-000000000000", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "95fbe98e-e054-4391-9eac-97ee6d17323d", "00000000-0000-0000-0000-000000000000", "Student", "STUDENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "db5ae76c-ffc0-4a3f-be98-1cb9b5f99c4f", "00000000-0000-0000-0000-000000000000", "Supervisor", "SUPERVISOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "335a2c1a-f76e-49f1-b29c-ea54721c0c50");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95fbe98e-e054-4391-9eac-97ee6d17323d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "db5ae76c-ffc0-4a3f-be98-1cb9b5f99c4f");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Supervisors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "184ab5c2-1617-44fd-80ce-ffaa7e5dd72e", "00000000-0000-0000-0000-000000000000", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8544235a-7fe5-4e40-90f6-8d4666d4077b", "00000000-0000-0000-0000-000000000000", "Student", "STUDENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b3d5bafa-ba47-4efd-8513-3ed6fc15032b", "00000000-0000-0000-0000-000000000000", "Supervisor", "SUPERVISOR" });
        }
    }
}
