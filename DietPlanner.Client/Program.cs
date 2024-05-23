using DietPlanner.Client;
using DietPlanner.Client.Common;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var apiSettings = builder.Configuration.GetSection("ApiBaseAddress").Value;
builder.Services.AddSingleton(new ApiSettings { ApiBaseAddress = apiSettings });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiSettings) });

builder.Services.AddMudServices();

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

await builder.Build().RunAsync();
