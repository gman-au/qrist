using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Qrist.Api.Host;
using Qrist.Api.Host.Injection;

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

app
    .MapHealthCheck()
    .MapQrCodeBuilderRequest();

app
    .Run();