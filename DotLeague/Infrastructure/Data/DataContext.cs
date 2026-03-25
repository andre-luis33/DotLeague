using System;
using System.Collections.Generic;
using System.Linq;
using DotLeague.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotLeague.Infrastructure.Data;

public class DataContext : DbContext
{
	public DbSet<Team> Team { get; set; }
	public DbSet<League> League { get; set; }
	public DbSet<Match> Match { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite("Data Source=db.sqlite");
		base.OnConfiguring(optionsBuilder);
	}

	/// <summary>
	/// Only seeds the database if no team or league has been added.
	/// </summary>
	public void Seed()
	{
		try
		{
			if(this.Team.Any() || this.League.Any())
				return;

			Console.WriteLine("Seeding database...");

			List<Team> teams =
			[
				new Team { Name = "Vasco da Gama", State = "Rio de Janeiro", City = "Rio de Janeiro", Stadium = "São Januário" },
				new Team { Name = "Flamengo", State = "Rio de Janeiro", City = "Rio de Janeiro", Stadium = "Maracanã" },
				new Team { Name = "Botafogo", State = "Rio de Janeiro", City = "Rio de Janeiro", Stadium = "Nilton Santos" },
				new Team { Name = "Fluminense", State = "Rio de Janeiro", City = "Rio de Janeiro", Stadium = "Maracanã" }
			];

			League league = new ()
			{
				Name = "Campeonato Carioca",
				ReferenceYear = 2025,
				StartDate = DateOnly.Parse("2025-01-01"),
				EndDate = DateOnly.Parse("2025-03-01"),
			};

			this.Team.AddRange(teams);
			this.League.Add(league);

			this.SaveChanges();

			Guid vascoId = teams[0].Id;
			Guid flamengoId = teams[1].Id;
			Guid botafogoId = teams[2].Id;
			Guid fluminenseId = teams[3].Id;

			Guid leagueId = league.Id;

			List<Match> matches = new List<Match>
			{
				new Match
				{
					HomeTeamId = vascoId,
					AwayTeamId = flamengoId,
					HomeTeamScore = 2,
					AwayTeamScore = 1,
					Date = new DateTime(2025, 1, 10, 16, 0, 0),
					LeagueId = leagueId
				},
				new Match
				{
					HomeTeamId = fluminenseId,
					AwayTeamId = botafogoId,
					HomeTeamScore = 0,
					AwayTeamScore = 1,
					Date = new DateTime(2025, 1, 10, 16, 0, 0),
					LeagueId = leagueId
				},
				new Match
				{
					HomeTeamId = vascoId,
					AwayTeamId = fluminenseId,
					HomeTeamScore = 2,
					AwayTeamScore = 0,
					Date = new DateTime(2025, 1, 15, 20, 0, 0),
					LeagueId = leagueId
				},
				new Match
				{
					HomeTeamId = botafogoId,
					AwayTeamId = flamengoId,
					HomeTeamScore = 3,
					AwayTeamScore = 2,
					Date = new DateTime(2025, 1, 15, 20, 0, 0),
					LeagueId = leagueId
				},
				new Match
				{
					HomeTeamId = botafogoId,
					AwayTeamId = vascoId,
					HomeTeamScore = 2,
					AwayTeamScore = 2,
					Date = new DateTime(2025, 1, 20, 16, 30, 0),
					LeagueId = leagueId
				},
				new Match
				{
					HomeTeamId = flamengoId,
					AwayTeamId = fluminenseId,
					HomeTeamScore = 2,
					AwayTeamScore = 1,
					Date = new DateTime(2025, 1, 20, 16, 30, 0),
					LeagueId = leagueId
				}
			};

			this.Match.AddRange(matches);
			this.SaveChanges();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Failed to seed database, with the following exception: {ex.Message}");
		}
	}

	public bool IsConnectionAvailable()
	{
		try
		{
			this.Database.OpenConnection();
			this.Database.CloseConnection();
			return true;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Failed to connect to the database, with the following exception: {ex.Message}");
			return false;
		}
	}
}
