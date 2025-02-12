using System;
using DotLeague.Infrastructure.Entities;

namespace DotLeague.Api.Dtos.Response;

public class TeamResponseDto
{
	public Guid Id { get; set; }
	public string? Name { get; set; }
	public string? State { get; set; }
	public string? Stadium { get; set; }

	public static TeamResponseDto FromEntity(Team team)
	{
		return new ()
		{
			Id = team.Id,
			Name = team.Name,
			State = team.State,
			Stadium = team.Stadium
		};
	}
}
