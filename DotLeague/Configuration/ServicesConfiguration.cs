using DotLeague.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DotLeague.Configuration;

public static class ServicesConfiguration
{
	public static void AddServices(this IServiceCollection services)
	{
		services.AddScoped<TeamService>();
		services.AddScoped<LeagueService>();
		services.AddScoped<MatchService>();
		services.AddScoped<HealthService>();
	}
}
