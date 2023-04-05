using BattlePlanner3000.Mappers;
using BattlePlanner3000.Models;

namespace BattlePlanner3000.Providers;

public class BattlePlanProvider
{

	private readonly DbProvider dbProvider;
	public BattlePlanProvider(DbProvider dbProvider)
	{
		this.dbProvider = dbProvider;
	}


	public async Task<List<BattlePlan>> GetBattlePlanAsync()
	{
		List<BattlePlan> PlanList = new List<BattlePlan>();
		var query = $@"SELECT b.battleplan_id, b.title_battleplan, b.duration, u.title_unit, u.unit_id
									FROM {Tables.BattlePlans} b
									JOIN battleplan_unit bu ON b.battleplan_id= bu.battleplan_id
                  JOIN unit u ON bu.unit_id= u.unit_id";
		var data = await dbProvider.QueryGetDataAsync(query, (reader, columnIndexes) => BattlePlanMappers.GetBattlePlan(reader, columnIndexes, PlanList));
		return data;
	}
	public async Task<List<BattlePlan>> FindBattlePlanAsync(string input)
	{
		List<BattlePlan> PlanList = new List<BattlePlan>();
		var query = $@"SELECT b.battleplan_id, b.title_battleplan, b.duration, u.title_unit, u.unit_id
									FROM {Tables.BattlePlans} b
									JOIN battleplan_unit bu ON b.battleplan_id= bu.battleplan_id
                  JOIN unit u ON bu.unit_id= u.unit_id
									where {Columns.BattlePlan.Title}='{input}'";
		var data = await dbProvider.QueryGetDataAsync(query, (reader, columnIndexes) => BattlePlanMappers.GetBattlePlan(reader, columnIndexes, PlanList));
		return data;
	}

	public async Task<List<BattlePlan>> SearchBattlePlansAsync(string input)
	{
		List<BattlePlan> PlanList = new List<BattlePlan>();
		var query = $@"SELECT b.battleplan_id, b.title_battleplan, b.duration, u.title_unit, u.unit_id
									FROM {Tables.BattlePlans} b
									JOIN battleplan_unit bu ON b.battleplan_id= bu.battleplan_id
                  JOIN unit u ON bu.unit_id= u.unit_id
									where lower({Columns.BattlePlan.Title}) like lower('{input}%')";
		var data = await dbProvider.QueryGetDataAsync(query, (reader, columnIndexes) => BattlePlanMappers.GetBattlePlan(reader, columnIndexes, PlanList));
		return data;
	}
	public async Task DeleteBattlePlanAsync(string input)
	{
		var query = $"DELETE FROM {Tables.BattlePlans} WHERE {Columns.BattlePlan.Title}='{input}'";
		await dbProvider.QueryExecuteAsync(query);
	}
	public async Task InsertBattlePlanAsync(string planName, int unitId, int duration)
	{
		var planValues = new Dictionary<string, object>()
		{
			{ Columns.BattlePlan.Title, planName},
			{Columns.BattlePlan.Duration,duration}
		};
		var planId = await dbProvider.InsertItemAsync(Tables.BattlePlans, planValues, Columns.BattlePlan.Id);

		var planUnitValues = new Dictionary<string, object>()
		{
			{ Columns.BattlePlanUnit.BattlePlanId, planId},
			{ Columns.BattlePlanUnit.UnitId, unitId}
		};

		await dbProvider.InsertItemAsync(Tables.BattlePlanUnits, planUnitValues);
	}
}