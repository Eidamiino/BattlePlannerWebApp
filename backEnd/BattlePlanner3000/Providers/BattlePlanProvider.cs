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
                  JOIN unit u ON bu.unit_id= u.unit_id
									order by {Columns.BattlePlan.Title}";
		var data = await dbProvider.QueryGetDataAsync(query, (reader, columnIndexes) => BattlePlanMappers.GetBattlePlan(reader, columnIndexes, PlanList));
		return data;
	}

	public async Task<List<RequirementAmountTotal>> GetSummary(string name)
	{
		BattlePlan plan = (await FindBattlePlanAsync(name)).Distinct().ToList()[0];
		List<RequirementAmountTotal> summary = new List<RequirementAmountTotal>();
		var query = $@"select r2.title, rr.requirement_id, sum(rr.amount) as dayAmount, sum(rr.amount*b.duration) as amount
								from battleplan b
								join battleplan_unit bu on b.battleplan_id = bu.battleplan_id
								JOIN unit u ON bu.unit_id= u.unit_id
								join unit_resource ur on u.unit_id = ur.unit_id
								join resource r on ur.resource_id = r.resource_id
								join resource_requirement rr on r.resource_id = rr.resource_id
								join requirement r2 on rr.requirement_id = r2.requirement_id
								where b.battleplan_id={plan.Id}
								group by rr.requirement_id,r2.title";
		var data = await dbProvider.QueryGetDataAsync(query,
			(reader, columnIndexes) => BattlePlanMappers.GetItemSummary(reader, columnIndexes, summary));
		return data;
	}
	public async Task<List<BattlePlan>> FindBattlePlanAsync(string input)
	{
		List<BattlePlan> PlanList = new List<BattlePlan>();
		var query = $@"SELECT b.battleplan_id, b.title_battleplan, b.duration, u.title_unit, u.unit_id
									FROM {Tables.BattlePlans} b
									JOIN battleplan_unit bu ON b.battleplan_id= bu.battleplan_id
                  JOIN unit u ON bu.unit_id= u.unit_id
									where {Columns.BattlePlan.Title}='{input}'
									order by {Columns.BattlePlan.Title}";
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
									where lower({Columns.BattlePlan.Title}) like lower('{input}%')
									order by {Columns.BattlePlan.Title}";
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
	public async Task AddItemToList(int planId, int unitId)
	{
		var query = $"insert into {Tables.BattlePlanUnits}" +
		            $"({Columns.BattlePlanUnit.BattlePlanId}, {Columns.BattlePlanUnit.UnitId})" +
		            $" values ({planId},{unitId})";
		await dbProvider.QueryExecuteAsync(query);
	}
	public async Task DeleteUnitFromListAsync(int planId, int unitId)
	{
		var query = $"DELETE FROM {Tables.BattlePlanUnits}" +
		            $" WHERE {Columns.BattlePlanUnit.BattlePlanId}={planId}" +
		            $" and {Columns.BattlePlanUnit.UnitId}={unitId}";
		await dbProvider.QueryExecuteAsync(query);
	}
}