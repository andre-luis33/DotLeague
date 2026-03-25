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
