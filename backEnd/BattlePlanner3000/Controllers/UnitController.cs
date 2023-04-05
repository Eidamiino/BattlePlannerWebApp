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
		// Unit unit = new Unit(input.Unit.Name, new List<ResourceAmount>());
		// provider.AddUnit(unit);
		// unit.ResourceList.Add(new ResourceAmount(resourceProvider.FindResource(input.ResourceAmount.Resource.Name),input.ResourceAmount.Amount));

		// await provider.InsertResourceAsync(input.UnitName, input.ResourceId, input.ResourceCapacity);
		await provider.InsertUnitAsync(input.UnitName, input.ResourceId, input.ResourceCapacity);

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