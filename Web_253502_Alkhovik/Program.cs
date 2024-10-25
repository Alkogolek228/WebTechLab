using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Web_253502_Alkhovik.Extensions;
using Web_253502_Alkhovik.Helpers;
using Web_253502_Alkhovik.Services.Authentication;
using Web_253502_Alkhovik.Services.CategoryService;
using Web_253502_Alkhovik.Services.CarService;
using Web_253502_Alkhovik.Services.FileService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.RegisterCustomServices();

builder.Services.Configure<UriData>(builder.Configuration.GetSection("UriData"));

//builder.Services.AddScoped<ICategoryService, ApiCategoryService>()
 //               .AddScoped<ICarService, ApiCarService>()
  //              .AddScoped<IFileService, ApiFileService>();

builder.Services.AddHttpClient("api", client =>
{
    var uriData = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<UriData>>().Value;
    client.BaseAddress = new Uri(uriData.ApiUri);
});

builder.Services.AddHttpClient("api").ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
    };
});

builder.Services.AddHttpClient("filesapi", client =>
{
    var uriData = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<UriData>>().Value;
    client.BaseAddress = new Uri($"{uriData.ApiUri}Files");
});

var keyclokData = builder.Configuration.GetSection("Keycloak").Get<KeycloakData>();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
    .AddCookie()
    .AddJwtBearer()
    .AddOpenIdConnect(opt =>
    {
        opt.Authority = $"{keyclokData.Host}/auth/realms/{keyclokData.Realm}";
        opt.ClientId = keyclokData.ClientId;
        opt.ClientSecret = keyclokData.ClientSecret;
        opt.ResponseType = OpenIdConnectResponseType.Code;
        opt.Scope.Add("openid");
        opt.SaveTokens = true;
        opt.RequireHttpsMetadata = false;
        opt.MetadataAddress = $"{keyclokData.Host}/realms/{keyclokData.Realm}/.well-known/openid-configuration";
    });

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("admin", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("POWER-USER");
    });
});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();