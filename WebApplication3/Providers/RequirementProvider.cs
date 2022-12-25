using WebApplication3.Models;

namespace WebApplication3.Providers;

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
	public static Requirement FindRequirementByName(string name)
	{
		return RequirementsList.Find(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
	}

}