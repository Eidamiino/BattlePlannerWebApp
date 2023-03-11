namespace BattlePlanner3000.Models;

public class ResourceAmount
{
	public int Amount { get; set; }
	public Resource Resource{ get; set; }
 
	public ResourceAmount(Resource resource, int amount)
	{
		Amount = amount;
		Resource= resource;
	}
}