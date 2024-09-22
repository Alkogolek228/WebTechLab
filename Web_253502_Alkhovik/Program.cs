using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Web_253502_Alkhovik.Extensions;
using Web_253502_Alkhovik.Helpers;
using Web_253502_Alkhovik.Services.CategoryService;
using Web_253502_Alkhovik.Services.CarService;
using Web_253502_Alkhovik.Services.FileService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.RegisterCustomServices();

builder.Services.Configure<UriData>(builder.Configuration.GetSection("UriData"));

builder.Services.AddScoped<ICategoryService, ApiCategoryService>()
                .AddScoped<ICarService, ApiCarService>()
                .AddScoped<IFileService, ApiFileService>();

builder.Services.AddHttpClient("api", client =>
{
    var uriData = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<UriData>>().Value;
    client.BaseAddress = new Uri(uriData.ApiUri);
});

builder.Services.AddHttpClient("filesapi", client =>
{
    var uriData = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<UriData>>().Value;
    client.BaseAddress = new Uri($"{uriData.ApiUri}Files");
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

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();