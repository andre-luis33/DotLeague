using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotLeague.Migrations
{
	/// <inheritdoc />
	public partial class create_leagues_table : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "leagues",
				columns: table => new
				{
					id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
					name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
					reference_year = table.Column<int>(type: "int", nullable: false),
					start_date = table.Column<DateOnly>(type: "date", nullable: false),
					end_date = table.Column<DateOnly>(type: "date", nullable: false),
					created_at = table.Column<DateTime>(type: "datetime", nullable: false)
            },
				constraints: table =>
				{
					table.PrimaryKey("PK_leagues", x => x.id);
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "leagues");
		}
	}
}
