namespace BattlePlanner3000.Models
{
	public class ResourceRequirementAmount
	{
		public ResourceRequirementAmount(string resourceName, string requirementName, int requirementCapacity)
		{
			this.Resource = new Resource(resourceName, new List<RequirementAmount>());
			this.RequirementAmount = new RequirementAmount(new Requirement(requirementName),requirementCapacity);
		}

		public Resource Resource { get; set; }
		public RequirementAmount RequirementAmount { get; set; }

	}
}
