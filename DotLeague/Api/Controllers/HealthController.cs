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
[Route("api/health")]
public class HealthController : MainController
{
	private readonly HealthService _healthService;

	public HealthController(HealthService healthService)
	{
		_healthService = healthService;
	}

	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public ActionResult Index()
	{
		return Ok("API is healthy!!");
	}

	[HttpGet("database")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
	public ActionResult TestDatabaseConnection()
	{
		var isAvailable = _healthService.TestDatabaseConnection();
		if (!isAvailable)
			return ErrorResponse(503, "Database connection is not available");

		return Ok("Database is healthy!!");
	}
}
