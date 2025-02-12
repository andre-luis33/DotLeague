using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotLeague.Infrastructure.Entities;

[Table("matches")]
public class Match
{
	[Key]
	[Column("id")]
	public Guid Id { get; set; }

	[Column("league_id")]
	public Guid LeagueId { get; set; }

	[Column("home_team_id")]
	public Guid HomeTeamId { get; set; }

	[Column("away_team_id")]
	public Guid AwayTeamId { get; set; }

	[Required]
	[Column("home_team_score")]
	public int HomeTeamScore { get; set; }

	[Required]
	[Column("away_team_score")]
	public int AwayTeamScore { get; set; }

	[Required]
	[Column("date")]
	public DateTime Date { get; set; }

	[Required]
	[Column("created_at")]
	public DateTime CreatedAt { get; set; }

	[NotMapped]
	public string? LeagueName { get; set; }

	[NotMapped]
	public int? LeagueReferenceYear { get; set; }

	[NotMapped]
	public string? HomeTeamName { get; set; }

	[NotMapped]
	public string? AwayTeamName { get; set; }

	public Match()
	{
		Id = Guid.NewGuid();
		CreatedAt = DateTime.UtcNow;
	}
}

