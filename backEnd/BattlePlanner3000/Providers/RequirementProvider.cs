using BattlePlanner3000.Models;

namespace BattlePlanner3000.Providers;

public class RequirementProvider
{
	public static List<Requirement> RequirementsList { get; private set; } = new List<Requirement>();

	public RequirementProvider()
	{

	}

	public List<Requirement> GetRequirements()
	{
		return RequirementsList;
	}

	public void AddRequirement(string name)
	{
		RequirementsList.Add(new Requirement(name));
	}
	public void DeleteRequirement(string name)
	{
		var requirement = RequirementsList.Find(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
		RequirementsList.Remove(requirement);
	}
	public Requirement FindRequirement(string name)
	{
		return RequirementsList.Find(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
	}
	public List<Requirement> SearchRequirements(string query)
	{
		return RequirementsList.Where(item => item.Name.StartsWith(query,StringComparison.InvariantCultureIgnoreCase)).ToList();
	}

}