using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using DotLeague.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDatabase();
builder.Services.AddServices();

var app = builder.Build();

app.MapControllers();
app.UseHttpsRedirection();

app.ConfigureDatabase();
app.ConfigureSwagger();

app.Run();
