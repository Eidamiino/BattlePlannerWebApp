using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using BattlePlanner3000.Providers;

namespace BattlePlanner3000
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddCors(options =>
			{
				options.AddPolicy(name: MyAllowSpecificOrigins,
													policy =>
													{
														policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod();
													});
			});

			// Add services to the container.
			builder.Services.AddRazorPages();
			builder.Services.AddScoped<UnitProvider>();
			builder.Services.AddScoped<ResourceProvider>();
			builder.Services.AddScoped<RequirementProvider>();
			builder.Services.AddControllers().AddNewtonsoftJson();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

				c.TagActionsBy(api =>
				{
					if (api.GroupName != null)
					{
						return new[] { api.GroupName };
					}

					var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
					if (controllerActionDescriptor != null)
					{
						return new[] { controllerActionDescriptor.ControllerName };
					}

					throw new InvalidOperationException("Unable to determine tag for endpoint.");
				});
				c.DocInclusionPredicate((name, api) => true);
			});

			var app = builder.Build();


			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Error");
			}
			else
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseStaticFiles();

			app.UseRouting();
			app.UseCors(MyAllowSpecificOrigins);
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