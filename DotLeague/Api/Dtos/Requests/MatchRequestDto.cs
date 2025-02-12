using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DotLeague.Api.Dtos.Requests;

public class MatchRequestDto
{
	[Required]
	public required Guid LeagueId { get; set; }

	[Required]
	public Guid HomeTeamId { get; set; }

	[Required]
	public required Guid AwayTeamId { get; set; }

	[Required]
	[Range(0, int.MaxValue, ErrorMessage = "Home team score must be a positive number.")]
	public required int HomeTeamScore { get; set; }

	[Required]
	[Range(0, int.MaxValue, ErrorMessage = "Away team score must be a positive number.")]
	public required int AwayTeamScore { get; set; }

	[Required]
	public required DateTime Date { get; set; }
}
