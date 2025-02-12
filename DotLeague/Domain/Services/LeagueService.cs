using System;
using System.Collections.Generic;
using System.Linq;
using DotLeague.Api.Dtos.Requests;
using DotLeague.Api.Dtos.Response;
using DotLeague.Domain.Exceptions;
using DotLeague.Infrastructure.Entities;

namespace DotLeague.Domain.Services;

public class LeagueService
{
	private DataContext _context;

	public LeagueService(DataContext context, TeamService teamService)
	{
		_context = context;
	}

	public List<LeagueResponseDto> FindAll()
	{
		var leagues = _context
			.League
			.Select(league => new LeagueResponseDto()
			{
				Id = league.Id,
				Name = league.Name,
				ReferenceYear = league.ReferenceYear,
				StartDate = league.StartDate,
				EndDate = league.EndDate,
			})
			.ToList();

		return leagues;
	}

	public List<MatchResponseDto> FindAllMatches(Guid? leagueId = null)
	{
		var query = _context.Match
			.Join(_context.League,
				match => match.LeagueId,
				league => league.Id,
				(match, league) => new { match, league })
			.Join(_context.Team,
				m => m.match.HomeTeamId,
				homeTeam => homeTeam.Id,
				(m, homeTeam) => new { m.match, m.league, homeTeam })
			.Join(_context.Team,
				m => m.match.AwayTeamId,
				awayTeam => awayTeam.Id,
				(m, awayTeam) => new MatchResponseDto
				{
					Id            = m.match.Id,
					HomeTeamId    = m.match.HomeTeamId,
					AwayTeamId    = m.match.AwayTeamId,
					HomeTeamScore = m.match.HomeTeamScore,
					AwayTeamScore = m.match.AwayTeamScore,
					Date          = m.match.Date,
					HomeTeamName  = m.homeTeam.Name,
					AwayTeamName  = awayTeam.Name,
					LeagueId      = m.league.Id,
					LeagueName    = m.league.Name,
					LeagueReferenceYear = m.league.ReferenceYear,
				});

		if (leagueId != null)
			query = query.Where(match => match.LeagueId == leagueId);

		return query.ToList();
	}

	public List<RankingResponseDto> GenerateRanking(Guid id)
	{
		var matchesFromLeague = this.FindAllMatches(id);
		if (matchesFromLeague == null || matchesFromLeague.Count == 0)
			return [];

		Dictionary<Guid, RankingResponseDto> ranking = [];

		foreach (var match in matchesFromLeague)
		{
			if (!ranking.ContainsKey(match.HomeTeamId))
				ranking[match.HomeTeamId] = new RankingResponseDto { TeamId = match.HomeTeamId, TeamName = match.HomeTeamName! };

			if (!ranking.ContainsKey(match.AwayTeamId))
				ranking[match.AwayTeamId] = new RankingResponseDto { TeamId = match.AwayTeamId, TeamName = match.AwayTeamName! };

			var homeTeam = ranking[match.HomeTeamId];
			var awayTeam = ranking[match.AwayTeamId];

			int goalDifference = Math.Abs(match.HomeTeamScore - match.AwayTeamScore);

			if (match.HomeTeamScore == match.AwayTeamScore)
			{
				homeTeam.Ties++;
				awayTeam.Ties++;
				homeTeam.Points += 1;
				awayTeam.Points += 1;
			}
			else if (match.HomeTeamScore > match.AwayTeamScore)
			{
				homeTeam.Wins++;
				homeTeam.Points += 3;
				homeTeam.GoalDifference += goalDifference;
				awayTeam.Loses++;
				awayTeam.GoalDifference -= goalDifference;
			}
			else
			{
				awayTeam.Wins++;
				awayTeam.Points += 3;
				awayTeam.GoalDifference += goalDifference;
				homeTeam.Loses++;
				homeTeam.GoalDifference -= goalDifference;
			}
		}

		return ranking.Values
		  .OrderByDescending(rank => rank.Points)
		  .ThenByDescending(rank => rank.Wins)
		  .ThenByDescending(rank => rank.GoalDifference)
		  .Select((rank, index) =>
		  {
			  rank.Position = index + 1;
			  return rank;
		  })
		  .ToList();
	}

	public LeagueResponseDto Create(LeagueRequestDto league)
	{
		if (league.StartDate > league.EndDate)
			throw new ServiceException("The end date must be greater than start date");

		var leagueExists = this.FindByNameAndReferenceYear(league.Name, league.ReferenceYear) != null;
		if (leagueExists)
			throw new ServiceException("Failed to create because the provided league name and reference year already exists");

		League leagueEntity = new()
		{
			Name = league.Name,
			ReferenceYear = league.ReferenceYear,
			StartDate = league.StartDate,
			EndDate = league.EndDate,
		};

		_context.League.Add(leagueEntity);
		_context.SaveChanges();

		return LeagueResponseDto.FromEntity(leagueEntity);
	}

	public void Update(Guid id, LeagueRequestDto league)
	{
		if (league.StartDate > league.EndDate)
			throw new ServiceException("The end date must be greater than start date");

		var leagueExists = this.FindByNameAndReferenceYear(league.Name, league.ReferenceYear);
		if (leagueExists != null && leagueExists.Id != id)
			throw new ServiceException("Failed to update because the provided league name and reference year already exists");

		var leagueEntity = this.FindById(id);
		if (leagueEntity == null)
			throw new ServiceException("Failed to update because the provided team does not exist");

		leagueEntity.Name = league.Name;
		leagueEntity.ReferenceYear = league.ReferenceYear;
		leagueEntity.StartDate = league.StartDate;
		leagueEntity.EndDate = league.EndDate;

		_context.League.Update(leagueEntity);
		_context.SaveChanges();

		return;
	}

	public void Delete(Guid id)
	{
		var leagueEntity = this.FindById(id);
		if(leagueEntity == null)
			throw new ServiceException("Failed to delete because the provided team does not exist");

		_context.League.Remove(leagueEntity);
		_context.SaveChanges();

		return;
	}

	public League? FindById(Guid id)
	{
		return _context.League
			.Where(league => league.Id.Equals(id))
			.FirstOrDefault();
	}

	public League? FindByNameAndReferenceYear(string name, int referenceYear)
	{
		return _context.League
			.Where(league => league.Name.Equals(name) && league.ReferenceYear.Equals(referenceYear))
			.FirstOrDefault();
	}
}
