namespace BattlePlanner3000.Models;

public class Unit
{
	public int Id { get; set; }
	public string Name { get; set; }
	public List<ResourceAmount> ResourceList { get; set; }

	public Unit(int id, string name, List<ResourceAmount> resourceList)
	{
		Id = id;
		Name = name;
		ResourceList = resourceList;
	}

}