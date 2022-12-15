using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class removdstdId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Projects");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "118ef520-d19b-4e1e-b46a-798d6c0003f1", "AQAAAAEAACcQAAAAELyPzCt5UeYaUTx9vUZP/TK3mN3lwYbQSG5zzxJPFrr1Gq7cuOBLKzvTxw2QdsCdAQ==", "a0077f02-638c-40d8-a649-36ca00931d07" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "458f667d-e61b-484a-bf1e-4e373037dea4", "AQAAAAEAACcQAAAAEMtdaRzqojK69Mrr9NrWz4Z1t+f7q1ex6csXM4wB1S5HrEO2xY+Kf5RsQru6StrKLw==", "80934f71-81f7-4331-8505-7d01bff0f21c" });
        }
    }
}
