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
[Route("api/matches")]
public class MatchController : MainController
{

	private MatchService _matchService;

	public MatchController(MatchService MatchService)
	{
		_matchService = MatchService;
	}

	[HttpGet]
	[ProducesResponseType(typeof(List<MatchResponseDto>), StatusCodes.Status200OK)]
	public ActionResult Index([FromQuery] Guid? leagueId)
	{
		var matches = _matchService.FindAll(leagueId);
		return Ok(matches);
	}

	[HttpPost]
	[ProducesResponseType(typeof(MatchResponseDto), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public ActionResult Store([FromBody] MatchRequestDto match)
	{
		try
		{
			var newMatch = _matchService.Create(match);
			return Created($"/leagues/{newMatch.Id}", newMatch);
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
	public ActionResult Update([FromRoute] Guid id, [FromBody] MatchRequestDto match)
	{
		try
		{
			_matchService.Update(id, match);
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
			_matchService.Delete(id);
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

