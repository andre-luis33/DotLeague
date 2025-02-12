using System;
using DotLeague.Infrastructure.Entities;

namespace DotLeague.Api.Dtos.Response;

public class MatchResponseDto
{
	public Guid Id { get; set; }
	public Guid HomeTeamId { get; set; }
	public Guid AwayTeamId { get; set; }
	public int HomeTeamScore { get; set; }
	public int AwayTeamScore { get; set; }
	public string? HomeTeamName { get; set; }
	public string? AwayTeamName { get; set; }
	public DateTime Date { get; set; }
	public Guid LeagueId { get; set; }
	public string? LeagueName { get; set; }
	public int? LeagueReferenceYear { get; set; }

	public static MatchResponseDto FromEntity(Match match)
	{
		return new()
		{
			Id = match.Id,
			HomeTeamId = match.HomeTeamId,
			AwayTeamId = match.AwayTeamId,
			HomeTeamScore = match.HomeTeamScore,
			AwayTeamScore = match.AwayTeamScore,
			HomeTeamName = match.HomeTeamName,
			AwayTeamName = match.AwayTeamName,
			Date = match.Date,
			LeagueId = match.LeagueId,
			LeagueName = match.LeagueName,
			LeagueReferenceYear = match.LeagueReferenceYear,
		};
	}

}
