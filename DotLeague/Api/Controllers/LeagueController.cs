using System;
using System.Collections.Generic;
using DotLeague.Api.Dtos.Requests;
using DotLeague.Api.Dtos.Response;
using DotLeague.Domain.Exceptions;
using DotLeague.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotLeague.Api.Controllers;

[ApiController]
[Route("api/leagues")]
public class LeagueController : MainController
{
	private readonly LeagueService _leagueService;

	public LeagueController(LeagueService LeagueService)
	{
		_leagueService = LeagueService;
	}

	[HttpGet]
	[ProducesResponseType(typeof(List<LeagueResponseDto>), StatusCodes.Status200OK)]
	public ActionResult Index()
	{
		var leagues = _leagueService.FindAll();
		return Ok(leagues);
	}

	[HttpGet("{id}/ranking")]
	[ProducesResponseType(typeof(List<RankingResponseDto>), StatusCodes.Status200OK)]
	public ActionResult Ranking([FromRoute] Guid id)
	{
		var rank = _leagueService.GenerateRanking(id);
		return Ok(rank);
	}

	[HttpPost]
	[ProducesResponseType(typeof(LeagueResponseDto), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public ActionResult Store([FromBody] LeagueRequestDto league)
	{
		try
		{
			var newLeague = _leagueService.Create(league);
			return Created($"/leagues/{newLeague.Id}", newLeague);
		}
		catch (ServiceException ex)
		{
			return this.ErrorResponse(400, ex.Message);
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
			return this.ErrorResponse(500, "Internal Server Error");
		}
	}

	[HttpPut("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public ActionResult Update([FromRoute] Guid id, [FromBody] LeagueRequestDto League)
	{
		try
		{
			_leagueService.Update(id, League);
			return NoContent();
		}
		catch (ServiceException ex)
		{
			return this.ErrorResponse(400, ex.Message);
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
			return this.ErrorResponse(500, "Internal Server Error");
		}
	}

	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public ActionResult Delete([FromRoute] Guid id)
	{
		try
		{
			_leagueService.Delete(id);
			return NoContent();
		}
		catch (ServiceException ex)
		{
			return this.ErrorResponse(400, ex.Message);
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
			return this.ErrorResponse(500, "Internal Server Error");
		}
	}

}

