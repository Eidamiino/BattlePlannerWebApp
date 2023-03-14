using BattlePlanner3000.Models;

namespace BattlePlanner3000.Providers;

public class BattlePlanProvider
{
	public static List<BattlePlan> BattlePlanList { get; private set; } = new List<BattlePlan>();
	public BattlePlanProvider()
	{
		
	}

	public List<BattlePlan> GetBattlePlans()
	{
		return BattlePlanList;
	}
	public void AddBattlePlan(BattlePlan battlePlan)
	{
		BattlePlanList.Add(battlePlan);
	}
	public BattlePlan FindBattlePlan(string name)
	{
		return BattlePlanList.Find(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
	}
	public void DeleteBattlePlan(string name)
	{
		var battlePlan= BattlePlanList.Find(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
		BattlePlanList.Remove(battlePlan);
	}
	public List<BattlePlan> SearchBattlePlan(string query)
	{
		return BattlePlanList.Where(item => item.Name.StartsWith(query, StringComparison.InvariantCultureIgnoreCase)).ToList();
	}
}