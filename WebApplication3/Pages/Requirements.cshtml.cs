using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.Models;
using WebApplication3.Providers;

namespace WebApplication3.Pages
{
	public class Requirements
	{
		private readonly RequirementProvider _provider;
		public List<Requirement> Items { get; set; }
		public int TotalItems { get; set; }
		[BindProperty] public string NewRequirementName { get; set; }

		public Requirements(RequirementProvider provider)
		{
			this._provider = provider;
		}

		public void OnGet()
		{
			LoadItems();
		}
		public void OnPost(string newRequirementName)
		{
			_provider.AddRequirement(newRequirementName);
			LoadItems();
			newRequirementName = String.Empty;
		}
		private void LoadItems()
		{
			Items = _provider.GetRequirements();
			TotalItems = Items?.Count ?? 0;
		}
	}
}
