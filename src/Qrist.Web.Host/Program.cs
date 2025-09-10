using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Qrist.Injection;
using Qrist.Web.Host.Components;

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
    .AddRazorComponents()
    .AddInteractiveServerComponents();

services
    .AddQristServices(configuration);

services
    .AddControllers();

var app =
    builder
        .Build();

if (!app.Environment.IsDevelopment())
{
    app
        .UseExceptionHandler("/Error", true);

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.

    app
        .UseHsts();
}

app
    .UseHttpsRedirection();

app
    .UseAntiforgery();

app
    .MapControllers();

app
    .MapStaticAssets();

app
    .MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app
    .Run();