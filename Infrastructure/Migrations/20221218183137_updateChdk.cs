using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
	public partial class updateChdk : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "AspNetRoles",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetRoles", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUsers",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
					ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
					UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
					PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
					SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
					ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
					PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
					TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
					LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
					LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
					AccessFailedCount = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUsers", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Departments",
				columns: table => new
				{
					DepartmentId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Departments", x => x.DepartmentId);
				});

			migrationBuilder.CreateTable(
				name: "AspNetRoleClaims",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					RoleId = table.Column<int>(type: "int", nullable: false),
					ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
					ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
					table.ForeignKey(
						name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
						column: x => x.RoleId,
						principalTable: "AspNetRoles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserClaims",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserId = table.Column<int>(type: "int", nullable: false),
					ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
					ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
					table.ForeignKey(
						name: "FK_AspNetUserClaims_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserLogins",
				columns: table => new
				{
					LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
					ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
					ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
					UserId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
					table.ForeignKey(
						name: "FK_AspNetUserLogins_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserRoles",
				columns: table => new
				{
					UserId = table.Column<int>(type: "int", nullable: false),
					RoleId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
					table.ForeignKey(
						name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
						column: x => x.RoleId,
						principalTable: "AspNetRoles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_AspNetUserRoles_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserTokens",
				columns: table => new
				{
					UserId = table.Column<int>(type: "int", nullable: false),
					LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
					Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
					Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
					table.ForeignKey(
						name: "FK_AspNetUserTokens_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Message",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
					When = table.Column<DateTime>(type: "datetime2", nullable: false),
					UserId = table.Column<int>(type: "int", nullable: false)
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

			migrationBuilder.CreateTable(
				name: "Supervisors",
				columns: table => new
				{
					SupervisorId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					FileNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
					UserId = table.Column<int>(type: "int", nullable: true),
					FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
					PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
					DepartmentId = table.Column<int>(type: "int", nullable: true),
					ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Supervisors", x => x.SupervisorId);
					table.ForeignKey(
						name: "FK_Supervisors_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_Supervisors_Departments_DepartmentId",
						column: x => x.DepartmentId,
						principalTable: "Departments",
						principalColumn: "DepartmentId");
				});

			migrationBuilder.CreateTable(
				name: "Notifications",
				columns: table => new
				{
					NotificationId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
					When = table.Column<DateTime>(type: "datetime2", nullable: false),
					IsRead = table.Column<bool>(type: "bit", nullable: false),
					SupervisorId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Notifications", x => x.NotificationId);
					table.ForeignKey(
						name: "FK_Notifications_Supervisors_SupervisorId",
						column: x => x.SupervisorId,
						principalTable: "Supervisors",
						principalColumn: "SupervisorId",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "ProjectArchive",
				columns: table => new
				{
					ProjectArchiveId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
					ProjectCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
					CaseStudy = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Year = table.Column<string>(type: "nvarchar(max)", nullable: false),
					DepartmentId = table.Column<int>(type: "int", nullable: true),
					FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
					DateSubmitted = table.Column<DateTime>(type: "datetime2", nullable: false),
					SupervisorId = table.Column<int>(type: "int", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ProjectArchive", x => x.ProjectArchiveId);
					table.ForeignKey(
						name: "FK_ProjectArchive_Departments_DepartmentId",
						column: x => x.DepartmentId,
						principalTable: "Departments",
						principalColumn: "DepartmentId");
					table.ForeignKey(
						name: "FK_ProjectArchive_Supervisors_SupervisorId",
						column: x => x.SupervisorId,
						principalTable: "Supervisors",
						principalColumn: "SupervisorId");
				});

			migrationBuilder.CreateTable(
				name: "Projects",
				columns: table => new
				{
					ProjectId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Topic = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
					FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
					DateSubmitted = table.Column<DateTime>(type: "datetime2", nullable: false),
					SupervisorId = table.Column<int>(type: "int", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Projects", x => x.ProjectId);
					table.ForeignKey(
						name: "FK_Projects_Supervisors_SupervisorId",
						column: x => x.SupervisorId,
						principalTable: "Supervisors",
						principalColumn: "SupervisorId");
				});

			migrationBuilder.CreateTable(
				name: "Students",
				columns: table => new
				{
					StudentId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserId = table.Column<int>(type: "int", nullable: false),
					MatricNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Level = table.Column<string>(type: "nvarchar(max)", nullable: false),
					SupervisorId = table.Column<int>(type: "int", nullable: true),
					ProjectArchiveId = table.Column<int>(type: "int", nullable: true),
					FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
					PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
					DepartmentId = table.Column<int>(type: "int", nullable: true),
					ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Students", x => x.StudentId);
					table.ForeignKey(
						name: "FK_Students_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Students_Departments_DepartmentId",
						column: x => x.DepartmentId,
						principalTable: "Departments",
						principalColumn: "DepartmentId");
					table.ForeignKey(
						name: "FK_Students_ProjectArchive_ProjectArchiveId",
						column: x => x.ProjectArchiveId,
						principalTable: "ProjectArchive",
						principalColumn: "ProjectArchiveId");
					table.ForeignKey(
						name: "FK_Students_Supervisors_SupervisorId",
						column: x => x.SupervisorId,
						principalTable: "Supervisors",
						principalColumn: "SupervisorId");
				});

			migrationBuilder.CreateTable(
				name: "Chapters",
				columns: table => new
				{
					ChapterId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Topic = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
					ChapterName = table.Column<short>(type: "smallint", nullable: false),
					ProjectId = table.Column<int>(type: "int", nullable: false),
					FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
					DateSubmitted = table.Column<DateTime>(type: "datetime2", nullable: false),
					SupervisorId = table.Column<int>(type: "int", nullable: true)
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
					table.ForeignKey(
						name: "FK_Chapters_Supervisors_SupervisorId",
						column: x => x.SupervisorId,
						principalTable: "Supervisors",
						principalColumn: "SupervisorId");
				});

			migrationBuilder.CreateTable(
				name: "ProjectStudent",
				columns: table => new
				{
					ProjectsProjectId = table.Column<int>(type: "int", nullable: false),
					StudentsStudentId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ProjectStudent", x => new { x.ProjectsProjectId, x.StudentsStudentId });
					table.ForeignKey(
						name: "FK_ProjectStudent_Projects_ProjectsProjectId",
						column: x => x.ProjectsProjectId,
						principalTable: "Projects",
						principalColumn: "ProjectId",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_ProjectStudent_Students_StudentsStudentId",
						column: x => x.StudentsStudentId,
						principalTable: "Students",
						principalColumn: "StudentId",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.InsertData(
				table: "AspNetRoles",
				columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
				values: new object[,]
				{
					{ 1, "00000000-0000-0000-0000-000000000000", "Admin", "ADMIN" },
					{ 2, "00000000-0000-0000-0000-000000000000", "Supervisor", "SUPERVISOR" },
					{ 3, "00000000-0000-0000-0000-000000000000", "Student", "STUDENT" }
				});

			migrationBuilder.InsertData(
				table: "AspNetUsers",
				columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "ImageUrl", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
				values: new object[] { 1, 0, "43e535b8-5e44-4389-bc8e-d2675ae684c9", "admin@gmail.com", true, " Super Admin", "https://cdn-icons-png.flaticon.com/512/3135/3135755.png", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEBJUxsJkYDsJMssw3Kpviad+HJ6fuYOrTDa4/AoERcyVMn1VR8H2hNjoju3C617CCA==", "1234567890", false, "7a608a3d-ffd5-47f9-8b7a-69771bcb6dcc", false, "Admin" });

			migrationBuilder.InsertData(
				table: "Departments",
				columns: new[] { "DepartmentId", "Name" },
				values: new object[] { 1, "Computer Science" });

			migrationBuilder.InsertData(
				table: "AspNetUserRoles",
				columns: new[] { "RoleId", "UserId" },
				values: new object[] { 1, 1 });

			migrationBuilder.CreateIndex(
				name: "IX_AspNetRoleClaims_RoleId",
				table: "AspNetRoleClaims",
				column: "RoleId");

			migrationBuilder.CreateIndex(
				name: "RoleNameIndex",
				table: "AspNetRoles",
				column: "NormalizedName",
				unique: true,
				filter: "[NormalizedName] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserClaims_UserId",
				table: "AspNetUserClaims",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserLogins_UserId",
				table: "AspNetUserLogins",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserRoles_RoleId",
				table: "AspNetUserRoles",
				column: "RoleId");

			migrationBuilder.CreateIndex(
				name: "EmailIndex",
				table: "AspNetUsers",
				column: "NormalizedEmail");

			migrationBuilder.CreateIndex(
				name: "UserNameIndex",
				table: "AspNetUsers",
				column: "NormalizedUserName",
				unique: true,
				filter: "[NormalizedUserName] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_Chapters_ProjectId",
				table: "Chapters",
				column: "ProjectId");

			migrationBuilder.CreateIndex(
				name: "IX_Chapters_SupervisorId",
				table: "Chapters",
				column: "SupervisorId");

			migrationBuilder.CreateIndex(
				name: "IX_Message_UserId",
				table: "Message",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_Notifications_SupervisorId",
				table: "Notifications",
				column: "SupervisorId");

			migrationBuilder.CreateIndex(
				name: "IX_ProjectArchive_DepartmentId",
				table: "ProjectArchive",
				column: "DepartmentId");

			migrationBuilder.CreateIndex(
				name: "IX_ProjectArchive_SupervisorId",
				table: "ProjectArchive",
				column: "SupervisorId");

			migrationBuilder.CreateIndex(
				name: "IX_Projects_SupervisorId",
				table: "Projects",
				column: "SupervisorId");

			migrationBuilder.CreateIndex(
				name: "IX_ProjectStudent_StudentsStudentId",
				table: "ProjectStudent",
				column: "StudentsStudentId");

			migrationBuilder.CreateIndex(
				name: "IX_Students_DepartmentId",
				table: "Students",
				column: "DepartmentId");

			migrationBuilder.CreateIndex(
				name: "IX_Students_ProjectArchiveId",
				table: "Students",
				column: "ProjectArchiveId");

			migrationBuilder.CreateIndex(
				name: "IX_Students_SupervisorId",
				table: "Students",
				column: "SupervisorId");

			migrationBuilder.CreateIndex(
				name: "IX_Students_UserId",
				table: "Students",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_Supervisors_DepartmentId",
				table: "Supervisors",
				column: "DepartmentId");

			migrationBuilder.CreateIndex(
				name: "IX_Supervisors_UserId",
				table: "Supervisors",
				column: "UserId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "AspNetRoleClaims");

			migrationBuilder.DropTable(
				name: "AspNetUserClaims");

			migrationBuilder.DropTable(
				name: "AspNetUserLogins");

			migrationBuilder.DropTable(
				name: "AspNetUserRoles");

			migrationBuilder.DropTable(
				name: "AspNetUserTokens");

			migrationBuilder.DropTable(
				name: "Chapters");

			migrationBuilder.DropTable(
				name: "Message");

			migrationBuilder.DropTable(
				name: "Notifications");

			migrationBuilder.DropTable(
				name: "ProjectStudent");

			migrationBuilder.DropTable(
				name: "AspNetRoles");

			migrationBuilder.DropTable(
				name: "Projects");

			migrationBuilder.DropTable(
				name: "Students");

			migrationBuilder.DropTable(
				name: "ProjectArchive");

			migrationBuilder.DropTable(
				name: "Supervisors");

			migrationBuilder.DropTable(
				name: "AspNetUsers");

			migrationBuilder.DropTable(
				name: "Departments");
		}
	}
}