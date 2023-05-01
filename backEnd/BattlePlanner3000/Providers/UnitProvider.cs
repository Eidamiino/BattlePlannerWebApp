using BattlePlanner3000.Mappers;
using BattlePlanner3000.Models;
using Npgsql;

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
		var query = $@"SELECT u.{Columns.Unit.Id}, ur.{Columns.UnitResource.Amount}, r.{Columns.Resource.Title}, u.{Columns.Unit.Title}, r.{Columns.Resource.Id}
									FROM {Tables.Units} u
									JOIN {Tables.UnitResources} ur ON u.{Columns.Unit.Id}= ur.{Columns.UnitResource.UnitId}
                  JOIN {Tables.Resources} r ON ur.{Columns.UnitResource.ResourceId}= r.{Columns.Resource.Id}
									order by {Columns.Unit.Title}";
		var data = await dbProvider.QueryGetDataAsync(query,
			(reader, columnIndexes) => UnitMappers.GetUnit(reader, columnIndexes, UnitList));
		return data;
	}

	public async Task<List<Unit>> FindUnitAsync(string input)
	{
		List<Unit> UnitList = new List<Unit>();
		var query = $@"SELECT u.{Columns.Unit.Id}, ur.{Columns.UnitResource.Amount}, r.{Columns.Resource.Title}, u.{Columns.Unit.Title}, r.{Columns.Resource.Id}
									FROM {Tables.Units} u
									JOIN {Tables.UnitResources} ur ON u.{Columns.Unit.Id}= ur.{Columns.UnitResource.UnitId}
                  JOIN {Tables.Resources} r ON ur.{Columns.UnitResource.ResourceId}= r.{Columns.Resource.Id}
									where {Columns.Unit.Title}='{input}'
									order by {Columns.UnitResource.Amount} desc";
		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter("@input", NpgsqlTypes.NpgsqlDbType.Text) { Value = input }
		};
		var data = await dbProvider.QueryGetDataAsync(query,
			(reader, columnIndexes) => reader.GetUnit(columnIndexes, UnitList),
			parameters);
		return data;
	}

	public async Task<List<Unit>> SearchUnitsAsync(string input)
	{
		List<Unit> UnitList = new List<Unit>();
		var query = $@"SELECT u.{Columns.Unit.Id}, ur.{Columns.UnitResource.Amount}, r.{Columns.Resource.Title}, u.{Columns.Unit.Title}, r.{Columns.Resource.Id}
									FROM {Tables.Units} u
									JOIN {Tables.UnitResources} ur ON u.{Columns.Unit.Id}= ur.{Columns.UnitResource.UnitId}
                  JOIN {Tables.Resources} r ON ur.{Columns.UnitResource.ResourceId}= r.{Columns.Resource.Id}
									where lower({Columns.Unit.Title}) like lower('{input}%')
									order by {Columns.Unit.Title}";
		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter("@input", NpgsqlTypes.NpgsqlDbType.Text) { Value = input.ToLower() + "%" }
		};
		var data = await dbProvider.QueryGetDataAsync(query,
			(reader, columnIndexes) => reader.GetUnit(columnIndexes, UnitList),
			parameters);
		return data;
	}
	public async Task<List<BattlePlan>> GetPlansWithUnitAsync(int unitId)
	{
		List<BattlePlan> PlanList= new List<BattlePlan>();
		var query = $@"SELECT b.{Columns.BattlePlan.Id}, b.{Columns.BattlePlan.Title}
									FROM {Tables.BattlePlans} b
									JOIN {Tables.BattlePlanUnits} bu ON b.{Columns.BattlePlan.Id} = bu.{Columns.BattlePlanUnit.BattlePlanId}									
									where {Columns.BattlePlanUnit.UnitId}={unitId}";
		var data = await dbProvider.QueryGetDataAsync(query, (reader, columnIndexes) => reader.GetPlainBattlePlan(columnIndexes, PlanList));
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
	public async Task DeleteResourceFromListAsync(int unitId, int resourceId)
	{
		var query = $"DELETE FROM {Tables.UnitResources}" +
		            $" WHERE {Columns.UnitResource.UnitId}={unitId}" +
		            $" and {Columns.UnitResource.ResourceId}={resourceId}";
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