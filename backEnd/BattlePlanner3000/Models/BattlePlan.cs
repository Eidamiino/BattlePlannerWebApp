namespace BattlePlanner3000.Models;

public class BattlePlan
{
	public int Id { get; set; }
	public string Name { get; set; }
	public List<Unit> UnitList { get; set; }
	public int Duration { get; set; }
	public Dictionary<string, int> Summary { get; private set; } = new Dictionary<string, int>();

	public BattlePlan(int id, string name, List<Unit> unitList, int duration)
	{
		Id = id;
		Name = name;
		UnitList = unitList;
		Duration = duration;
		CalculateSummary();
	}
	public void CalculateSummary()
	{
		Summary = new Dictionary<string, int>();
		foreach (var unit in UnitList)
		{
			foreach (var resource in unit.ResourceList)
			{
				foreach (var requirement in resource.Resource.RequirementList)
				{
					if (!Summary.ContainsKey(requirement.Requirement.Name))
						Summary.Add(requirement.Requirement.Name, 0);

					var num = requirement.Amount * resource.Amount * Duration;
					Summary[requirement.Requirement.Name] += num;
				}
			}
		}

	}

}