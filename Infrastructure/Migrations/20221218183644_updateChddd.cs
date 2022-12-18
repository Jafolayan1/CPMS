using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
	public partial class updateChddd : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Message");

			migrationBuilder.UpdateData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: 1,
				columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
				values: new object[] { "d9685336-4169-48bb-abb7-dc74c07b0785", "AQAAAAEAACcQAAAAEKr6DDTYoQiSMwCMbGEvrvS5gjD+VpmU2Sd2MoiCn/q646eq7iX5ZQhXZoTRgg3YdA==", "ab649980-ca2c-460b-8382-eb42c416955a" });
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Message",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserId = table.Column<int>(type: "int", nullable: false),
					Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
					When = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Message", x => x.Id);
					table.ForeignKey(
						name: "FK_Message_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.UpdateData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: 1,
				columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
				values: new object[] { "43e535b8-5e44-4389-bc8e-d2675ae684c9", "AQAAAAEAACcQAAAAEBJUxsJkYDsJMssw3Kpviad+HJ6fuYOrTDa4/AoERcyVMn1VR8H2hNjoju3C617CCA==", "7a608a3d-ffd5-47f9-8b7a-69771bcb6dcc" });

			migrationBuilder.CreateIndex(
				name: "IX_Message_UserId",
				table: "Message",
				column: "UserId");
		}
	}
}