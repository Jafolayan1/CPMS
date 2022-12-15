using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class rmoveMatric : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Matric",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Matric",
                table: "ProjectArchive");

            migrationBuilder.DropColumn(
                name: "Matric",
                table: "Chapters");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dce16130-e706-4fd5-85d7-77b6f74d144f", "AQAAAAEAACcQAAAAEO4TNSx8DPiaPMGjr8DwIbRMAWo12nKguiGtcuGHXOS2lfQ6+vkqwfGPE5hXgkdx8A==", "a9fce1cb-46d0-47a8-8fea-ce259fef7ed5" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Matric",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Matric",
                table: "ProjectArchive",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
                values: new object[] { "118ef520-d19b-4e1e-b46a-798d6c0003f1", "AQAAAAEAACcQAAAAELyPzCt5UeYaUTx9vUZP/TK3mN3lwYbQSG5zzxJPFrr1Gq7cuOBLKzvTxw2QdsCdAQ==", "a0077f02-638c-40d8-a649-36ca00931d07" });
        }
    }
}
