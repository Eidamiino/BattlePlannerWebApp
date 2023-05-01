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
		var query = $@"SELECT b.{Columns.BattlePlan.Id}, b.{Columns.BattlePlan.Title}, b.{Columns.BattlePlan.Duration}, u.{Columns.Unit.Title}, u.{Columns.Unit.Id}
									FROM {Tables.BattlePlans} b
									JOIN {Tables.BattlePlanUnits} bu ON b.{Columns.BattlePlan.Id}= bu.{Columns.BattlePlanUnit.BattlePlanId}
                  JOIN {Tables.Units} u ON bu.{Columns.BattlePlanUnit.UnitId}= u.{Columns.Unit.Id}
									order by {Columns.BattlePlan.Title}";
		var data = await dbProvider.QueryGetDataAsync(query, (reader, columnIndexes) => BattlePlanMappers.GetBattlePlan(reader, columnIndexes, PlanList));
		return data;
	}

	public async Task<List<RequirementAmountTotal>> GetSummary(string name)
	{
		BattlePlan plan = (await FindBattlePlanAsync(name)).Distinct().ToList()[0];
		List<RequirementAmountTotal> summary = new List<RequirementAmountTotal>();
		var query = $@"select r2.{Columns.Requirement.Title}, rr.{Columns.ResourceRequirement.RequirementId}, sum(rr.{Columns.ResourceRequirement.Amount}) as dayAmount, sum(rr.{Columns.ResourceRequirement.Amount}*b.{Columns.BattlePlan.Duration}) as amount
								from {Tables.BattlePlans} b
								join {Tables.BattlePlanUnits} bu on b.{Columns.BattlePlan.Id} = bu.{Columns.BattlePlanUnit.BattlePlanId}
								JOIN {Tables.Units} u ON bu.{Columns.BattlePlanUnit.UnitId}= u.{Columns.Unit.Id}
								join {Tables.UnitResources} ur on u.{Columns.Unit.Id}= ur.{Columns.UnitResource.UnitId}
								join {Tables.Resources} r on ur.{Columns.UnitResource.ResourceId}= r.{Columns.Resource.Id}
								join {Tables.ResourceRequirements} rr on r.{Columns.Resource.Id}= rr.{Columns.ResourceRequirement.ResourceId}
								join {Tables.Requirements} r2 on rr.{Columns.ResourceRequirement.RequirementId}= r2.{Columns.Requirement.Id}
								where b.{Columns.BattlePlan.Id}={plan.Id}
								group by rr.{Columns.ResourceRequirement.RequirementId},r2.{Columns.Requirement.Title}";
		var data = await dbProvider.QueryGetDataAsync(query,
			(reader, columnIndexes) => BattlePlanMappers.GetItemSummary(reader, columnIndexes, summary));
		return data;
	}
	public async Task<List<BattlePlan>> FindBattlePlanAsync(string input)
	{
		List<BattlePlan> PlanList = new List<BattlePlan>();
		var query = $@"SELECT b.{Columns.BattlePlan.Id}, b.{Columns.BattlePlan.Title}, b.{Columns.BattlePlan.Duration}, u.{Columns.Unit.Title}, u.{Columns.Unit.Id}
									FROM {Tables.BattlePlans} b
									JOIN {Tables.BattlePlanUnits} bu ON b.{Columns.BattlePlan.Id}= bu.{Columns.BattlePlanUnit.BattlePlanId}
                  JOIN {Tables.Units} u ON bu.{Columns.BattlePlanUnit.UnitId}= u.{Columns.Unit.Id}
									where {Columns.BattlePlan.Title}='{input}'
									order by {Columns.BattlePlan.Title}";
		var data = await dbProvider.QueryGetDataAsync(query, (reader, columnIndexes) => BattlePlanMappers.GetBattlePlan(reader, columnIndexes, PlanList));
		return data;
	}

	public async Task<List<BattlePlan>> SearchBattlePlansAsync(string input)
	{
		List<BattlePlan> PlanList = new List<BattlePlan>();
		var query = $@"SELECT b.{Columns.BattlePlan.Id}, b.{Columns.BattlePlan.Title}, b.{Columns.BattlePlan.Duration}, u.{Columns.Unit.Title}, u.{Columns.Unit.Id}
									FROM {Tables.BattlePlans} b
									JOIN {Tables.BattlePlanUnits} bu ON b.{Columns.BattlePlan.Id}= bu.{Columns.BattlePlanUnit.BattlePlanId}
                  JOIN {Tables.Units} u ON bu.{Columns.BattlePlanUnit.UnitId}= u.{Columns.Unit.Id}
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