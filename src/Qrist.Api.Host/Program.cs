using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Qrist.Api.Host;
using Qrist.Injection;

var builder =
    WebApplication
        .CreateBuilder(args);

var services =
    builder
        .Services;

var configuration =
    builder
        .Configuration;

services
    .AddOpenApi();

services
    .AddQristServices(configuration);

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

app
    .MapHealthCheck()
    .MapQrCodeBuilderRequests()
    .MapQrCodeProcessorRequests();

app
    .Run();