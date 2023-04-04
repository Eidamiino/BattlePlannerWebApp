namespace BattlePlanner3000.Models
{
	public class UnitResourceAmount
	{
		public UnitResourceAmount(string unitName, string resourceName, int resourceCapacity)
		{
			this.Unit = new Unit(unitName, new List<ResourceAmount>());
			this.ResourceAmount= new ResourceAmount(new Resource(1,resourceName,new List<RequirementAmount>()), resourceCapacity);
		}

		public Unit Unit{ get; set; }
		public ResourceAmount ResourceAmount{ get; set; }

	}
}
