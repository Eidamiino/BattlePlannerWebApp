using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BattlePlanner3000.Models;
using BattlePlanner3000.Providers;

namespace BattlePlanner3000.Pages
{
	public class Resources
	{
		private readonly ResourceProvider _provider;
		private readonly RequirementProvider _requirementProvider;
		public List<Resource> Items { get; set; }
		public int TotalItems { get; set; }
		[BindProperty] public string NewResourceName { get; set; }
		[BindProperty] public string RequirementName { get; set; }
		[BindProperty] public int RequirementCapacity { get; set; }

		public Resources(ResourceProvider provider, RequirementProvider requirementProvider)
		{
			this._provider = provider;
			_requirementProvider = requirementProvider;
		}

		public void OnGet()
		{
			LoadItems();
		}

		public void OnPost(string newResourceName, string newRequirementName, int newRequirementCapacity)
		{
			if (newRequirementCapacity < 1)
			{
			
			}
			Resource resource = new Resource(newResourceName, new List<RequirementAmount>());
			RequirementAmount requirementAmount =
				new RequirementAmount(_requirementProvider.FindRequirementByName(newRequirementName), newRequirementCapacity);
			_provider.AddResource(resource);
			resource.RequirementList.Add(requirementAmount);
			LoadItems();
		}
		private void LoadItems()
		{
			Items = _provider.GetResources();
			TotalItems = Items?.Count ?? 0;
		}

	}

}
