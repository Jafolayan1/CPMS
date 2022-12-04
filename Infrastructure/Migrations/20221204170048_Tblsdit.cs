using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class Tblsdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_UserId",
                table: "Students");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Supervisors",
                keyColumn: "SupervisorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "OtherNames",
                table: "Supervisors");

            migrationBuilder.DropColumn(
                name: "OtherNames",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "OtherNames",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Supervisors",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Students",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "AspNetUsers",
                newName: "FullName");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "FullName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ec7124bf-0e2f-41b3-be3b-d72f472da64f", " Super Admin", "AQAAAAEAACcQAAAAEAY5IFvK9Tz4xgN1kO2+iEmJ7DHhjJ7KOmjSx47LGRit8eVt6KoiNerDpPwuMrt7Bw==", "ecec12c8-9292-4bd7-a5d6-c4954a5215b6" });

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AspNetUsers_UserId",
                table: "Students",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_UserId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Supervisors",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Students",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "AspNetUsers",
                newName: "Surname");

            migrationBuilder.AddColumn<string>(
                name: "OtherNames",
                table: "Supervisors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "OtherNames",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherNames",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "OtherNames", "PasswordHash", "SecurityStamp", "Surname" },
                values: new object[] { "5b2ed8f1-1365-4c2a-89d7-4c9ba0759462", "Admin", "AQAAAAEAACcQAAAAEB0qjjZ62m9OeRqMR4+etR96WyO/HiBpx1zRQMpJ+/JQ3MncgV1METmVuLHXbmh3Nw==", "9fc45074-1391-4045-a8dd-e72194ddf6f5", "Super " });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "ImageUrl", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OtherNames", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { 2, 0, "03abffc1-db2f-4f1b-bc80-8483f623cdaf", "afolayan.oluwatosin20@gmail.com", true, "https://cdn-icons-png.flaticon.com/512/3135/3135755.png", false, null, "AFOLAYAN.OLUWATOSIN20@GMAIL.COM", "EM20200104321", "Adekunle Adewale", "AQAAAAEAACcQAAAAEGOYvihvjzE/Huq63edGt6M8VBXDpYnI33DutVkqvtP2p2E/AfnWZy6r5WYGUGaB8w==", "1234567890", false, "e5fa1b4f-c517-445e-98ac-1e7f5c816d90", "Uthman", false, "EM20200104321" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "Name" },
                values: new object[,]
                {
                    { 2, "Computer Engineering" },
                    { 3, "Statistics" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 2, 2 });

            migrationBuilder.InsertData(
                table: "Supervisors",
                columns: new[] { "SupervisorId", "Bio", "DepartmentId", "Email", "FileNo", "ImageUrl", "OtherNames", "PhoneNumber", "Surname", "UserId" },
                values: new object[] { 1, null, 1, "afolayan.oluwatosin20@gmail.com", "EM20200104321", "https://cdn-icons-png.flaticon.com/512/3135/3135755.png", "Adekunle Adewale", "1234567890", "Uthman", 2 });

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AspNetUsers_UserId",
                table: "Students",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
