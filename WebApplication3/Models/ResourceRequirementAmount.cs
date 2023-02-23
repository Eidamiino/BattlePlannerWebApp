namespace WebApplication3.Models
{
	public class ResourceRequirementAmount
	{
		public ResourceRequirementAmount(string resourceName, string requirementName, int requirementCapacity)
		{
			ResourceName = resourceName;
			RequirementName = requirementName;
			RequirementCapacity = requirementCapacity;
			this.Resource = new Resource(resourceName, new List<RequirementAmount>());
			this.RequirementAmount = new RequirementAmount(new Requirement(requirementName),requirementCapacity);
		}

		public Resource Resource { get; set; }
		public RequirementAmount RequirementAmount { get; set; }
		public string ResourceName { get; set; }
		public string RequirementName{ get; set; }
		public int RequirementCapacity{ get; set; }

	}
}
