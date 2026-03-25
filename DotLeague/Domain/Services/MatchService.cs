using System;
using System.Collections.Generic;
using System.Linq;
using DotLeague.Api.Dtos.Requests;
using DotLeague.Api.Dtos.Response;
using DotLeague.Domain.Exceptions;
using DotLeague.Infrastructure.Data;
using DotLeague.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotLeague.Domain.Services;

public class MatchService
{
	private readonly DataContext _context;
	private readonly TeamService _teamService;
	private readonly LeagueService _leagueService;

	public MatchService(DataContext context, TeamService teamService, LeagueService leagueService)
	{
		_context = context;
		_teamService = teamService;
		_leagueService = leagueService;
	}

	public List<MatchResponseDto> FindAll(Guid? leagueId)
	{
		return _leagueService.FindAllMatches(leagueId);
	}

	public MatchResponseDto Create(MatchRequestDto match)
	{
		this.ValidateMatchData(match);

		Match matchEntity = new()
		{
 			HomeTeamId    = match.HomeTeamId,
			AwayTeamId    = match.AwayTeamId,
			HomeTeamScore = match.HomeTeamScore,
			AwayTeamScore = match.AwayTeamScore,
			Date 			  = match.Date,
			LeagueId 	  = match.LeagueId,
		};

		_context.Match.Add(matchEntity);
		_context.SaveChanges();

		return MatchResponseDto.FromEntity(matchEntity);
	}

	public void Update(Guid id, MatchRequestDto match)
	{
		var matchEntity = this.FindById(id);
		if(matchEntity == null)
			throw new Exception("The provided match does not exist");

		this.ValidateMatchData(match);

		matchEntity.HomeTeamId    = match.HomeTeamId;
		matchEntity.AwayTeamId    = match.AwayTeamId;
		matchEntity.HomeTeamScore = match.HomeTeamScore;
		matchEntity.AwayTeamScore = match.AwayTeamScore;
		matchEntity.Date          = match.Date;

		_context.Match.Update(matchEntity);
		_context.SaveChanges();

		return;
	}

	private void ValidateMatchData(MatchRequestDto match)
	{
		if(match.AwayTeamId == match.HomeTeamId)
			throw new ServiceException("The provided home team id is the same as the away team id");

		var league = _leagueService.FindById(match.LeagueId);
		if(league == null)
			throw new ServiceException("The provided league does not exist");

		var matchDate = DateOnly.FromDateTime(match.Date);
		if(matchDate < league.StartDate || matchDate > league.EndDate)
			throw new ServiceException("The provided match date is outside of the league start/end date");

		var homeTeam = _teamService.FindById(match.HomeTeamId);
		if(homeTeam == null)
			throw new ServiceException("The provided home team does not exist");

		var awayTeam = _teamService.FindById(match.AwayTeamId);
		if(awayTeam == null)
			throw new ServiceException("The provided away team does not exist");

		bool homeTeamHasMatchOnThatDate = this.FindByTeamIdAndDate(match.HomeTeamId, match.Date) != null;
		if(homeTeamHasMatchOnThatDate)
			throw new ServiceException("The provided home team already has a match on the provided date");

		bool awayTeamHasMatchOnThatDate = this.FindByTeamIdAndDate(match.AwayTeamId, match.Date) != null;
		if(awayTeamHasMatchOnThatDate)
			throw new ServiceException("The provided away team already has a match on the provided date");
	}

	public void Delete(Guid id)
	{
		var matchEntity = this.FindById(id);
		if(matchEntity == null)
			throw new ServiceException("Failed to delete because the provided team does not exist");

		_context.Match.Remove(matchEntity);
		_context.SaveChanges();

		return;
	}

	public Match? FindById(Guid id)
	{
		return _context.Match
			.Where(match => match.Id.Equals(id))
			.FirstOrDefault();
	}

	public League? GetLeagueByNameAndReferenceYear(string name, int referenceYear)
	{
		return _context.League
			.Where(league => league.Name.Equals(name) && league.ReferenceYear.Equals(referenceYear))
			.FirstOrDefault();
	}

	public Match? FindByTeamIdAndDate(Guid teamId, DateTime date)
	{
		return _context.Match
			.Where(match =>
				match.Date.Date.Equals(date.Date) &&
				(match.HomeTeamId.Equals(teamId) || match.AwayTeamId.Equals(teamId))
			)
			.FirstOrDefault();
	}

}
