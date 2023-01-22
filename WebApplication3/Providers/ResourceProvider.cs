using WebApplication3.Models;

namespace WebApplication3.Providers;

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
	public Resource FindResourceByName(string name)
	{
		return ResourceList.Find(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
	}
}