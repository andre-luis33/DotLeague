using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace DotLeague.Configuration;

public static class SwaggerConfiguration
{

	public static void ConfigureSwagger(this WebApplication app)
	{
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();

			Console.WriteLine("######### Swagger UI: http://localhost:5183/swagger/index.html");
		}
	}

}
