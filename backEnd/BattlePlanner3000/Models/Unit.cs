namespace BattlePlanner3000.Models;

public class Unit
{
	public string Name { get; set; }
	public List<ResourceAmount> ResourceList { get; set; }

	public Unit(string name, List<ResourceAmount> resourceList)
	{
		Name = name;
		ResourceList = resourceList;
	}

}