using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotLeague.Migrations
{
	/// <inheritdoc />
	public partial class create_matches_table : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "matches",
				columns: table => new
				{
					id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
					league_id = table.Column<string>(type: "varchar(50)", nullable: false),
					home_team_id = table.Column<string>(type: "varchar(50)", nullable: false),
					away_team_id = table.Column<string>(type: "varchar(50)", nullable: false),
					home_team_score = table.Column<byte>(type: "tinyint", nullable: false),
					away_team_score = table.Column<byte>(type: "tinyint", nullable: false),
					date = table.Column<DateTime>(type: "datetime", nullable: false),
					created_at = table.Column<DateTime>(type: "datetime", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_matches", x => x.id);
					table.ForeignKey(
							name: "FK_matches_leagues_league_id",
							column: x => x.league_id,
							principalTable: "leagues",
							principalColumn: "id",
							onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
							name: "FK_matches_teams_home_team_id",
							column: x => x.home_team_id,
							principalTable: "teams",
							principalColumn: "id",
							onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
							name: "FK_matches_teams_away_team_id",
							column: x => x.away_team_id,
							principalTable: "teams",
							principalColumn: "id",
							onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_matches_league_id",
				table: "matches",
				column: "league_id");

			migrationBuilder.CreateIndex(
				name: "IX_matches_home_team_id",
				table: "matches",
				column: "home_team_id");

			migrationBuilder.CreateIndex(
				name: "IX_matches_away_team_id",
				table: "matches",
				column: "away_team_id");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(name: "matches");
		}
	}
}
