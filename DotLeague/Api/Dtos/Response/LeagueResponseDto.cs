using System;
using DotLeague.Infrastructure.Entities;

namespace DotLeague.Api.Dtos.Response;

public class LeagueResponseDto
{
	public Guid Id { get; set; }
	public string? Name { get; set; }
	public int ReferenceYear { get; set; }
	public DateOnly StartDate { get; set; }
	public DateOnly EndDate { get; set; }

	public static LeagueResponseDto FromEntity(League league)
	{
		return new ()
		{
			Id = league.Id,
			Name = league.Name,
			ReferenceYear = league.ReferenceYear,
			StartDate = league.StartDate,
			EndDate = league.EndDate,
		};
	}
}
