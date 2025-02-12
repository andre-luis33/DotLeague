using System;

namespace DotLeague.Api.Dtos.Response;

public class RankingResponseDto
{
	public int Position { get; set; }
	public int Points { get; set; }
	public int Matches { get; set; }
	public int Wins { get; set; }
	public int Loses { get; set; }
	public int Ties { get; set; }
	public int GoalDifference { get; set; }
	public Guid? TeamId { get; set; }
	public string TeamName { get; set; }

	public RankingResponseDto()
	{
		Points = 0;
		Matches = 0;
		Wins = 0;
		Loses = 0;
		Ties = 0;
		GoalDifference = 0;
		TeamName = String.Empty;
	}
}
