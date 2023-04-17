using System.Data;
using BattlePlanner3000.Models;
using BattlePlanner3000.Pages;
using BattlePlanner3000.Providers;

namespace BattlePlanner3000.Mappers;

public static class BattlePlanMappers
{
	public static BattlePlan GetBattlePlan(this IDataReader reader, Dictionary<string, int> columnIndexes, List<BattlePlan> plans)
	{
		var title = reader.GetString(columnIndexes[Columns.BattlePlan.Title]);

		var existingPlan = plans.FirstOrDefault(r => r.Name== title);
		if (existingPlan != null)
		{
			var unit = new Unit(reader.GetInt32(columnIndexes[Columns.Unit.Id]),
				reader.GetString(columnIndexes[Columns.Unit.Title]), new List<ResourceAmount>());
			existingPlan.UnitList.Add(unit);
			return existingPlan;
		}
		else
		{
			var planId = reader.GetInt32(columnIndexes[Columns.BattlePlan.Id]);
			var duration = reader.GetInt32(columnIndexes[Columns.BattlePlan.Duration]);
			var plan = new BattlePlan(planId, title, new List<Unit>(),duration);

			var unit = new Unit(reader.GetInt32(columnIndexes[Columns.Unit.Id]),
				reader.GetString(columnIndexes[Columns.Unit.Title]), new List<ResourceAmount>());
			plan.UnitList.Add(unit);

			plans.Add(plan);
			return plan;
		}
	}

	public static RequirementAmountTotal GetItemSummary(this IDataReader reader, Dictionary<string, int> columnIndexes,
		List<RequirementAmountTotal> list)
	{
		var title = reader.GetString(columnIndexes[Columns.Requirement.Title]);
		var id = reader.GetInt32(columnIndexes[Columns.ResourceRequirement.RequirementId]);
		var dayAmount = reader.GetInt32(columnIndexes["dayamount"]);
		var totalAmount = reader.GetInt32(columnIndexes["amount"]);

		RequirementAmountTotal requirementAmountTotal =
			new RequirementAmountTotal(new Requirement(id, title), dayAmount, totalAmount);
		list.Add(requirementAmountTotal);
		return requirementAmountTotal;
	}
}