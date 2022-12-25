using WebApplication3.Providers;

namespace WebApplication3
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddRazorPages();
			builder.Services.AddScoped<ResourceProvider>();
			builder.Services.AddScoped<RequirementProvider>();
			builder.Services.AddControllers();
			
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Error");
			}
			
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();
			
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
				endpoints.MapControllers();
				endpoints.MapGet("/robots.txt", async context => await context.Response.WriteAsync("User-Agent: *\nDisallow: /"));
			});

			app.Run();
		}
	}
}