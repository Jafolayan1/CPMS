using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
	public partial class tblupdatedd : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.UpdateData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: 1,
				columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
				values: new object[] { "9f1683ff-6eb7-47d7-8436-218a6e60f970", "AQAAAAEAACcQAAAAEL0AE7Rn5pAE9EH8aXMmfntKXt9MSZs0zdneqkO2HUbCatlxhJEfE2ulpeTChSQBzw==", "001af05c-e12a-4c44-a23d-4eb56db7742e" });
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.UpdateData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: 1,
				columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
				values: new object[] { "84e95ab8-567d-4d50-8812-2cda0a5d2b16", "AQAAAAEAACcQAAAAEPjarVXCw3MV4zvNAEql85mf9Le6CgMSYMhXwfxnn54xq3XuxIhyZKwf4+P9XbHaMg==", "aec658f4-737d-43cb-ae55-bd0573922c6e" });
		}
	}
}