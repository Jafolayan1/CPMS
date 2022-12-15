using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class projectTblUpdat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Students_StudentId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_StudentId",
                table: "Projects");

            migrationBuilder.CreateTable(
                name: "ProjectStudent",
                columns: table => new
                {
                    ProjectsProjectId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectStudent", x => new { x.ProjectsProjectId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_ProjectStudent_Projects_ProjectsProjectId",
                        column: x => x.ProjectsProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectStudent_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "458f667d-e61b-484a-bf1e-4e373037dea4", "AQAAAAEAACcQAAAAEMtdaRzqojK69Mrr9NrWz4Z1t+f7q1ex6csXM4wB1S5HrEO2xY+Kf5RsQru6StrKLw==", "80934f71-81f7-4331-8505-7d01bff0f21c" });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectStudent_StudentId",
                table: "ProjectStudent",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectStudent");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6f2ad747-8f6c-428c-85a1-41dab54398b7", "AQAAAAEAACcQAAAAEF3JdQY1wv7FTLjnLEInhm1cgkdiY5fXiReJANRs/vQNVFwoEW8GUYQ2UFk558dmIw==", "e388ba44-850c-40ee-a242-e1e1556a1b2f" });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_StudentId",
                table: "Projects",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Students_StudentId",
                table: "Projects",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
