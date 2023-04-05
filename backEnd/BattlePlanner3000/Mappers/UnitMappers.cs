using System.Data;
using BattlePlanner3000.Models;
using BattlePlanner3000.Pages;
using BattlePlanner3000.Providers;

namespace BattlePlanner3000.Mappers;

public static class UnitMappers
{
	public static Unit GetUnit(this IDataReader reader, Dictionary<string, int> columnIndexes, List<Unit> units)
	{
		var title = reader.GetString(columnIndexes[Columns.Unit.Title]);

		var existingUnit = units.FirstOrDefault(r => r.Name== title);
		if (existingUnit != null)
		{
			var resourceAmount = new ResourceAmount(new Resource(reader.GetInt32(columnIndexes[Columns.Resource.Id]),reader.GetString(columnIndexes[Columns.Resource.Title]),new List<RequirementAmount>()), reader.GetInt32(columnIndexes[Columns.UnitResource.Amount]));
			existingUnit.ResourceList.Add(resourceAmount);
			return existingUnit;
		}
		else
		{
			var unitId = reader.GetInt32(columnIndexes[Columns.Unit.Id]);

			var unit = new Unit(unitId, title, new List<ResourceAmount>());
			var resourceAmount= new ResourceAmount(new Resource(reader.GetInt32(columnIndexes[Columns.Resource.Id]), reader.GetString(columnIndexes[Columns.Resource.Title]),new List<RequirementAmount>()), reader.GetInt32(columnIndexes[Columns.UnitResource.Amount]));
			unit.ResourceList.Add(resourceAmount);
			units.Add(unit);
			return unit;
		}
	}
	public static Unit GetUnitNoList(this IDataReader reader, Dictionary<string, int> columnIndexes)
	{
		var unit= new Unit(reader.GetInt32(columnIndexes[Columns.Unit.Id]), reader.GetString(columnIndexes[Columns.Unit.Title]),new List<ResourceAmount>());
		return unit;
	}
}