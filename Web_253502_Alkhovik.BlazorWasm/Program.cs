using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Web_253502_Alkhovik.BlazorWasm;
using Web_253502_Alkhovik.BlazorWasm.Services;
using Web_253502_Alkhovik.BlazorWasm.Helpers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure DataUri
var dataUri = builder.Configuration.GetSection("DataUri").Get<DataUri>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(dataUri.ApiUri) });

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Keycloak", options.ProviderOptions);
    options.ProviderOptions.RedirectUri = "http://localhost:7003/authentication/login-callback";
});

builder.Services.AddScoped<IDataService, DataService>();

await builder.Build().RunAsync();