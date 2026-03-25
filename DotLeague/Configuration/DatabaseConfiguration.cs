using DotLeague.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DotLeague.Configuration;

public static class DatabaseConfiguration
{
	public static void AddDatabase(this IServiceCollection services)
	{
		services.AddScoped<DataContext>();
	}

	public static void ConfigureDatabase(this WebApplication app)
	{
		using var scope = app.Services.CreateScope();

		var context = scope.ServiceProvider.GetRequiredService<DataContext>();
		context.Database.Migrate();

		if (app.Environment.IsDevelopment())
		{
			context.Seed();
		}
	}

}
