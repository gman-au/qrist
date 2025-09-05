using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Qrist.Api.Host;
using Qrist.Api.Host.Injection;
using Qrist.Interfaces;

var builder =
    WebApplication
        .CreateBuilder(args);

var services =
    builder
        .Services;

services
    .AddOpenApi();

services
    .AddQristServices();

var app =
    builder
        .Build();

if (app.Environment.IsDevelopment())
{
    app
        .MapOpenApi();
}

app
    .UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app
    .MapHealthCheck();

app.MapGet("/weatherforecast", (ITodoistAdapter test) =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast");

app.Run();

namespace Qrist.Api.Host
{
    record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}