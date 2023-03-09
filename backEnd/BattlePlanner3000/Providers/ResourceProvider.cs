using BattlePlanner3000.Models;

namespace BattlePlanner3000.Providers;

public class ResourceProvider
{
	public static List<Resource> ResourceList { get; private set; } = new List<Resource>();
	public ResourceProvider()
	{
		
	}

	public List<Resource> GetResources()
	{
		return ResourceList;
	}
	public void AddResource(Resource resource)
	{
		ResourceList.Add(resource);
	}
	public Resource FindResource(string name)
	{
		return ResourceList.Find(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
	}
	public void DeleteResource(string name)
	{
		var resource= ResourceList.Find(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
		ResourceList.Remove(resource);
	}
}