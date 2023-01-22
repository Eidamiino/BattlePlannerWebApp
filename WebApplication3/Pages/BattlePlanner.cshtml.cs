using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.Models;
using WebApplication3.Providers;

namespace WebApplication3.Pages
{
	public class BattlePlannerModel : PageModel
	{
		private readonly RequirementProvider _requirementProvider;
		private readonly ResourceProvider _resourceProvider;
		public Requirements Requirements;
		public Resources Resources;
		[BindProperty] public string NewRequirementName { get; set;}
		[BindProperty] public string NewResourceName { get; set; }
		[BindProperty] public string RequirementName { get; set; }
		[BindProperty] public int RequirementCapacity { get; set; }

		public Microsoft.AspNetCore.Http.IFormCollection Form { get; set; }

		public BattlePlannerModel(RequirementProvider requirementProvider, ResourceProvider resourceProvider)
		{
			this._requirementProvider = requirementProvider;
			this._resourceProvider = resourceProvider;
			Requirements = new Requirements(_requirementProvider);
			Resources = new Resources(_resourceProvider,_requirementProvider);
		}
		public void OnGet()
		{
			Requirements.OnGet();
			Resources.OnGet();
		}

		public void OnPostCreateResource()
		{
			Resources.OnPost(NewResourceName,RequirementName,RequirementCapacity);
			Console.WriteLine(Form);
			OnGet();
		}

		public void OnPostCreateRequirement()
		{
			Requirements.OnPost(NewRequirementName);
			OnGet();
		}
	}
}
