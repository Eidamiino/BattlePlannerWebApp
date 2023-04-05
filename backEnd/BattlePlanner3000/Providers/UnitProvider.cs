using BattlePlanner3000.Mappers;
using BattlePlanner3000.Models;

namespace BattlePlanner3000.Providers;

public class UnitProvider
{
	private readonly DbProvider dbProvider;
	public UnitProvider(DbProvider dbProvider)
	{
		this.dbProvider = dbProvider;
	}
	public async Task<List<Unit>> GetUnitsAsync()
	{
		List<Unit> UnitList= new List<Unit>();
		var query = @"SELECT u.unit_id, ur.amount, r.title_resource, u.title_unit, r.resource_id
									FROM unit u
									JOIN unit_resource ur ON u.unit_id= ur.unit_id
                  JOIN resource r ON ur.resource_id= r.resource_id";
		var data = await dbProvider.QueryGetDataAsync(query, (reader, columnIndexes) => UnitMappers.GetUnit(reader, columnIndexes, UnitList));
		return data;
	}
	public async Task<List<Unit>> FindUnitAsync(string input)
	{
		List<Unit> UnitList= new List<Unit>();
		var query = $@"SELECT u.unit_id, ur.amount, r.title_resource, u.title_unit, r.resource_id
									FROM unit u
									JOIN unit_resource ur ON u.unit_id= ur.unit_id
                  JOIN resource r ON ur.resource_id= r.resource_id
									where {Columns.Unit.Title}='{input}'";
		var data = await dbProvider.QueryGetDataAsync(query,
			(reader, columnIndexes) => reader.GetUnit(columnIndexes, UnitList));
		return data;
	}

	public async Task<List<Unit>> SearchUnitsAsync(string input)
	{
		List<Unit> UnitList = new List<Unit>();
		var query = $@"SELECT u.unit_id, ur.amount, r.title_resource, u.title_unit, r.resource_id
									FROM unit u
									JOIN unit_resource ur ON u.unit_id= ur.unit_id
                  JOIN resource r ON ur.resource_id= r.resource_id
									where lower({Columns.Unit.Title}) like lower('{input}%')";
		var data = await dbProvider.QueryGetDataAsync(query,
			(reader, columnIndexes) => reader.GetUnit(columnIndexes, UnitList));
		return data;
	}
	public async Task InsertUnitAsync(string unitName, int resourceId, int amount)
	{
		var unitValues= new Dictionary<string, object>()
		{
			{ Columns.Unit.Title, unitName}
		};
		var unitId = await dbProvider.InsertItemAsync(Tables.Units, unitValues, Columns.Unit.Id);

		var unitResourceValues= new Dictionary<string, object>()
		{
			{ Columns.UnitResource.UnitId, unitId},
			{ Columns.UnitResource.ResourceId, resourceId},
			{ Columns.UnitResource.Amount, amount}
		};

		await dbProvider.InsertItemAsync(Tables.UnitResources, unitResourceValues);
	}
	public async Task DeleteUnitAsync(string input)
	{
		var query = $"DELETE FROM {Tables.Units} WHERE {Columns.Unit.Title}='{input}'";
		await dbProvider.QueryExecuteAsync(query);
	}








	public static List<Unit> UnitList { get; private set; } = new List<Unit>();

	public List<Unit> GetUnits()
	{
		return UnitList;
	}
	public void AddUnit(Unit unit)
	{
		UnitList.Add(unit);
	}
	public Unit FindUnit(string name)
	{
		return UnitList.Find(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
	}
	public void DeleteUnit(string name)
	{
		var unit= UnitList.Find(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
		UnitList.Remove(unit);
	}
	public List<Unit> SearchUnits(string query)
	{
		return UnitList.Where(item => item.Name.StartsWith(query, StringComparison.InvariantCultureIgnoreCase)).ToList();
	}
}