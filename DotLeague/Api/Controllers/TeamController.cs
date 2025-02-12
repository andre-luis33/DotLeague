using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DotLeague.Api.Dtos.Requests;
using DotLeague.Api.Dtos.Response;
using DotLeague.Domain.Exceptions;
using DotLeague.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotLeague.Api.Controllers;

[ApiController]
[Route("api/teams")]
public class TeamController : MainController
{
	private readonly TeamService _teamService;

	public TeamController(TeamService teamService)
	{
		_teamService = teamService;
	}

	[HttpGet]
	[ProducesResponseType(typeof(List<TeamResponseDto>), StatusCodes.Status200OK)]
	public ActionResult Index()
	{
		var teams = _teamService.FindAll();
		return Ok(teams);
	}

	[HttpPost]
	[ProducesResponseType(typeof(TeamResponseDto), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public ActionResult Store([FromBody] TeamRequestDto team)
	{
		try
		{
			var newTeam = _teamService.Create(team);
			return Ok(newTeam);
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
	public ActionResult Update([FromRoute] Guid id, [FromBody] TeamRequestDto team)
	{
		try
		{
			_teamService.Update(id, team);
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
			_teamService.Delete(id);
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

