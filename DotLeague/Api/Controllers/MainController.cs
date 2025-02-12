using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using DotLeague.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotLeague.Api.Controllers;

public class MainController : ControllerBase
{
	[NonAction]
	public ActionResult ErrorResponse(int status, string message)
	{
		return StatusCode(status, new { message });
	}
}
