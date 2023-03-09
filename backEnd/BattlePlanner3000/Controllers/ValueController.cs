using Microsoft.AspNetCore.Mvc;
using BattlePlanner3000.Models;

namespace BattlePlanner3000.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ValueController : ControllerBase
{
	// GET
	[HttpGet]
	[Route("resources")]
	public IActionResult GetResources()
	{
		return Ok(DateTime.Now);
	}
	[HttpPost]
	[Route("resources")]
	public IActionResult PostResources([FromBody]Requirement name)
	{
		return Ok(DateTime.Now);
	}
}