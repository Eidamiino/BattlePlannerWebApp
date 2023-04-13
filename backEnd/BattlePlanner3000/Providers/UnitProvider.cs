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
		List<Unit> UnitList = new List<Unit>();
		var query = $@"SELECT u.unit_id, ur.amount, r.title_resource, u.title_unit, r.resource_id
									FROM unit u
									JOIN unit_resource ur ON u.unit_id= ur.unit_id
                  JOIN resource r ON ur.resource_id= r.resource_id
									order by {Columns.Unit.Title}";
		var data = await dbProvider.QueryGetDataAsync(query,
			(reader, columnIndexes) => UnitMappers.GetUnit(reader, columnIndexes, UnitList));
		return data;
	}

	public async Task<List<Unit>> FindUnitAsync(string input)
	{
		List<Unit> UnitList = new List<Unit>();
		var query = $@"SELECT u.unit_id, ur.amount, r.title_resource, u.title_unit, r.resource_id
									FROM unit u
									JOIN unit_resource ur ON u.unit_id= ur.unit_id
                  JOIN resource r ON ur.resource_id= r.resource_id
									where {Columns.Unit.Title}='{input}'
									order by {Columns.Unit.Title}";
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
									where lower({Columns.Unit.Title}) like lower('{input}%')
									order by {Columns.Unit.Title}";
		var data = await dbProvider.QueryGetDataAsync(query,
			(reader, columnIndexes) => reader.GetUnit(columnIndexes, UnitList));
		return data;
	}

	public async Task InsertUnitAsync(string unitName, int resourceId, int amount)
	{
		var unitValues = new Dictionary<string, object>()
		{
			{ Columns.Unit.Title, unitName }
		};
		var unitId = await dbProvider.InsertItemAsync(Tables.Units, unitValues, Columns.Unit.Id);

		var unitResourceValues = new Dictionary<string, object>()
		{
			{ Columns.UnitResource.UnitId, unitId },
			{ Columns.UnitResource.ResourceId, resourceId },
			{ Columns.UnitResource.Amount, amount }
		};

		await dbProvider.InsertItemAsync(Tables.UnitResources, unitResourceValues);
	}

	public async Task AlterAmountAsync(int unitId, int resourceId, int newAmount)
	{
		var query =
			$"update {Tables.UnitResources} set {Columns.UnitResource.Amount}= {newAmount}" +
			$" where {Columns.UnitResource.UnitId}={unitId}" +
			$" and {Columns.UnitResource.ResourceId}={resourceId}";
		await dbProvider.QueryExecuteAsync(query);

	}

	public async Task DeleteUnitAsync(string input)
	{
		var query = $"DELETE FROM {Tables.Units} WHERE {Columns.Unit.Title}='{input}'";
		await dbProvider.QueryExecuteAsync(query);
	}

	public async Task AddItemToList(int unitId, int resourceId, int amount)
	{
		var query = $"insert into {Tables.UnitResources}" +
		            $"({Columns.UnitResource.UnitId}, {Columns.UnitResource.ResourceId}, {Columns.UnitResource.Amount})" +
		            $" values ({unitId},{resourceId},{amount})";
		await dbProvider.QueryExecuteAsync(query);
	}
}