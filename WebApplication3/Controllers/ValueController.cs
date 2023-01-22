using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers;

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