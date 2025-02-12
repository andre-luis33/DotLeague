using System;
using DotLeague.Api;
using DotLeague.Api.Controllers;
using DotLeague.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database Injection
builder.Services.AddScoped<DataContext>();

// Services Injection
builder.Services.AddScoped<TeamService>();
builder.Services.AddScoped<LeagueService>();
builder.Services.AddScoped<MatchService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();

	using (var scope = app.Services.CreateScope())
	{
		var context = scope.ServiceProvider.GetRequiredService<DataContext>();
		context.Seed();
	}
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();
