using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class chapteerrTBL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Chapter",
                table: "Projects");

            migrationBuilder.CreateTable(
                name: "Chapters",
                columns: table => new
                {
                    ChapterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChapterName = table.Column<short>(type: "smallint", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSubmitted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapters", x => x.ChapterId);
                    table.ForeignKey(
                        name: "FK_Chapters_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0bca9f82-6dd2-4894-b7c2-1ae17e15682f", "afolayan.oluwatosin20@gmail.com", "AFOLAYAN.OLUWATOSIN20@GMAIL.COM", "AQAAAAEAACcQAAAAEFLr3/FhLVUnV52hzO9ihsZWM43thPbAPPTMZCHK1amghQ0YHOg9LLIE/EDv//T53Q==", "6f1447e1-2bee-4a0c-9b83-8c849314f66b" });

            migrationBuilder.UpdateData(
                table: "Supervisors",
                keyColumn: "SupervisorId",
                keyValue: 1,
                column: "Email",
                value: "afolayan.oluwatosin20@gmail.com");

            migrationBuilder.CreateIndex(
                name: "IX_Chapters_ProjectId",
                table: "Chapters",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chapters");

            migrationBuilder.AddColumn<short>(
                name: "Chapter",
                table: "Projects",
                type: "smallint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ed3d4815-9e69-4754-bffa-5628a45b7d7d", "AQAAAAEAACcQAAAAENJFE884si+Ujhkxg0e+xZ1aYXToq3S5GKXOkmtsapx3WEUxDCEbJse/NT0JB0sLIg==", "1bd2a07c-02bb-41a4-b6e8-4c0394d125de" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b8c27520-cad2-4c35-8e7b-b0a605145469", "addeewale@gmail.com", "ADEWALE@GMAIL.COM", "AQAAAAEAACcQAAAAEIlufJSwMfrGNMBHS7FtpesxStxbgWwf6W9FwuhoVGHoJmRIKe3VQyFofapK5Bhtig==", "31103039-5f01-4fdf-8d2f-7877a3cdfd21" });

            migrationBuilder.UpdateData(
                table: "Supervisors",
                keyColumn: "SupervisorId",
                keyValue: 1,
                column: "Email",
                value: "addeewale@gmail.com");
        }
    }
}
