using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using BattlePlanner3000.Models;
using BattlePlanner3000.Pages;
using BattlePlanner3000.Providers;
using Newtonsoft.Json.Linq;

namespace BattlePlanner3000.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class BattlePlanController: ControllerBase
{
	private readonly BattlePlanProvider provider;
	private readonly UnitProvider unitProvider;

	public BattlePlanController(BattlePlanProvider provider, UnitProvider unitProvider)
	{
		this.provider = provider;
		this.unitProvider= unitProvider;
	}

	// GET
	[HttpGet]
	[Route("")]
	public async Task<IActionResult> GetBattlePlans()
	{
		return Ok((await provider.GetBattlePlanAsync()).Distinct());
	}

	[HttpGet]
	[Route("{name}")]
	public async Task<IActionResult> GetBattlePlan([FromRoute] string name, [FromQuery] bool returnList)
	{
		if (returnList) return Ok((await provider.SearchBattlePlansAsync(name)).Distinct());
		return Ok((await provider.FindBattlePlanAsync(name)).Distinct());
	}

	[HttpGet]
	[Route("{name}/summary")]
	public async Task<IActionResult> GetBattlePlanSummary([FromRoute] string name)
	{
		return Ok((await provider.GetSummary(name)).Distinct());
	}

	[HttpPost]
	[Route("")]
	public async Task<IActionResult> PostBattlePlan([FromBody] BattlePlanUnitDuration input)
	{
		await provider.InsertBattlePlanAsync(input.PlanName, input.UnitId, input.Duration);
		return Ok();
	}


	[HttpDelete]
	[Route("{name}")]
	public async Task<IActionResult> DeleteBattlePlan([FromRoute] string name)
	{
		await provider.DeleteBattlePlanAsync(name);
		return Ok();
	}
	[HttpPut]
	[Route("{name}/addItem")]
	public async Task<IActionResult> AddItemToListAsync([FromRoute] string name, [FromBody] int unitId)
	{
		var planId = (await provider.FindBattlePlanAsync(name)).Distinct().ToList();
		await provider.AddItemToList(planId[0].Id, unitId);
		return Ok();
	}
	[HttpDelete]
	[Route("{name}/removeItem/{unitId}")]
	public async Task<IActionResult> DeleteUnitFromListAsync([FromRoute] string name, [FromRoute] int unitId)
	{
		var plan= (await provider.FindBattlePlanAsync(name)).Distinct().ToList();
		await provider.DeleteUnitFromListAsync(plan[0].Id, unitId);
		return Ok();
	}
}