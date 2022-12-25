using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.Models;
using WebApplication3.Providers;

namespace WebApplication3.Pages
{
	public class Resources : PageModel
	{
		private readonly ResourceProvider _provider;
		public List<Resource> Items { get; set; }
		public int TotalItems { get; set; }
		[BindProperty] public string NewResourceName { get; set; }
		[BindProperty] public string RequirementName { get; set; }
		[BindProperty] public int RequirementCapacity { get; set; }

		public Resources(ResourceProvider provider)
		{
			this._provider = provider;

		}

		public void OnGet()
		{
			LoadItems();
		}

		public void OnPost(string newResourceName, string newRequirementName, int newRequirementCapacity)
		{
			Resource resource = new Resource(newResourceName, new Dictionary<Requirement, int>());
			_provider.AddResource(resource);
			resource.RequirementList.Add(RequirementProvider.FindRequirementByName(newRequirementName),newRequirementCapacity);
			LoadItems();
		}
		private void LoadItems()
		{
			Items = _provider.GetResources();
			TotalItems = Items?.Count ?? 0;
		}

	}

}
