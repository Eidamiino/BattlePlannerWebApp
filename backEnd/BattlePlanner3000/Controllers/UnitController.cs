using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using BattlePlanner3000.Models;
using BattlePlanner3000.Pages;
using BattlePlanner3000.Providers;
using Newtonsoft.Json.Linq;

namespace BattlePlanner3000.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UnitController : ControllerBase
{
	private readonly UnitProvider provider;
	private readonly ResourceProvider resourceProvider;

	public UnitController(UnitProvider provider, ResourceProvider resourceProvider)
	{
		this.provider = provider;
		this.resourceProvider = resourceProvider;
	}

	// GET
	[HttpGet]
	[Route("")]
	public async Task<IActionResult> GetUnits()
	{
		return Ok((await provider.GetUnitsAsync()).Distinct());
	}

	[HttpGet]
	[Route("{name}")]
	public async Task<IActionResult> GetUnit([FromRoute] string name, [FromQuery] bool returnList)
	{
		if (returnList) return Ok((await provider.SearchUnitsAsync(name)).Distinct());
		return Ok((await provider.FindUnitAsync(name)).Distinct());
	}

	[HttpPost]
	[Route("")]
	public async Task<IActionResult> PostUnit([FromBody] UnitResourceAmount input)
	{
		await provider.InsertUnitAsync(input.UnitName, input.ResourceId, input.ResourceCapacity);

		return Ok();
	}

	[HttpPut]
	[Route("{name}")]
	public async Task<IActionResult> UpdateAmountAsync([FromRoute] string name, [FromBody] IdAmount input)
	{
		var unitId = (await provider.FindUnitAsync(name)).Distinct().ToList();
		await provider.AlterAmountAsync(unitId[0].Id, input.Id, input.Amount);
		return Ok();
	}

	[HttpPut]
	[Route("{name}/addItem")]
	public async Task<IActionResult> AddItemToListAsync([FromRoute] string name, [FromBody] IdAmount input)
	{
		var unitId = (await provider.FindUnitAsync(name)).Distinct().ToList();
		await provider.AddItemToList(unitId[0].Id, input.Id, input.Amount);
		return Ok();
	}

	[HttpDelete]
	[Route("{name}")]
	public async Task<IActionResult> DeleteUnit([FromRoute] string name)
	{
		await provider.DeleteUnitAsync(name);
		return Ok();
	}
}