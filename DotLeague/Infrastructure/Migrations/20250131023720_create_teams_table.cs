using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotLeague.Migrations
{
	/// <inheritdoc />
	public partial class create_teams_table : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "teams",
				columns: table => new
				{
					id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
					name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
					state = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
					city = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
					stadium = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
					created_at = table.Column<DateTime>(type: "datetime", nullable: false),
					deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_teams", x => x.id);
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				 name: "teams");
		}
	}
}
