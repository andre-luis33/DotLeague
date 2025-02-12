using System;
using System.Collections.Generic;
using System.Linq;
using DotLeague.Api.Dtos.Requests;
using DotLeague.Api.Dtos.Response;
using DotLeague.Domain.Exceptions;
using DotLeague.Infrastructure.Entities;

namespace DotLeague.Domain.Services;

public class TeamService
{
	private DataContext _context;

	public TeamService(DataContext context)
	{
		_context = context;
	}

	public List<TeamResponseDto> FindAll()
	{
		var teams = _context
			.Team
			.OrderBy(team => team.Name)
			.Select(team => new TeamResponseDto()
			{
				Id = team.Id,
				Name = team.Name,
				State = team.State,
				Stadium = team.Stadium
			})
			.ToList();

		return teams;
	}

	public TeamResponseDto Create(TeamRequestDto team)
	{
		var teamExists = this.FindByName(team.Name) != null;
		if(teamExists)
			throw new ServiceException("Failed to create because the provided team name already exists");

		Team teamEntity = new()
		{
			Name = team.Name,
			State = team.State,
			City = team.City,
			Stadium = team.Stadium,
		};

		_context.Team.Add(teamEntity);
		_context.SaveChanges();

		return TeamResponseDto.FromEntity(teamEntity);
	}

	public void Update(Guid id, TeamRequestDto team)
	{
		var teamByName = this.FindByName(team.Name);
		if(teamByName != null && teamByName.Id != id)
			throw new ServiceException("Failed to update because the provided team name already exists");

		var teamEntity = this.FindById(id);
		if(teamEntity == null)
			throw new ServiceException("Failed to update because the provided team does not exist");

		teamEntity.Name = team.Name;
		teamEntity.City = team.City;
		teamEntity.State = team.State;
		teamEntity.Stadium = team.Stadium;

		_context.Team.Update(teamEntity);
		_context.SaveChanges();

		return;
	}

	public void Delete(Guid id)
	{
		var teamEntity = this.FindById(id);
		if(teamEntity == null)
			throw new ServiceException("Failed to delete because the provided team does not exist");

		_context.Team.Remove(teamEntity);
		_context.SaveChanges();

		return;
	}

	public Team? FindById(Guid id)
	{
		return _context.Team
			.Where(team => team.Id.Equals(id))
			.FirstOrDefault();
	}

	public Team? FindByName(string name)
	{
		return _context.Team
			.Where(team => team.Name.Equals(name))
			.FirstOrDefault();
	}

}
