using Microsoft.EntityFrameworkCore;
using Web_253502_Alkhovik.API.Data;
using Web_253502_Alkhovik.API.Services.CarService;
using Web_253502_Alkhovik.API.Services.CategoryService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Web_253502_Alkhovik.API.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=sppr;Username=postgres;Password=1234"));

builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

var authService = builder.Configuration.GetSection("AuthServer").Get<AuthServerData>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
    {
        opt.Authority = $"{authService.Host}/realms/{authService.Realm}";
        opt.MetadataAddress = $"{authService.Host}/realms/{authService.Realm}/.well-known/openid-configuration";
        opt.Audience = "account";
        opt.RequireHttpsMetadata = false;
        opt.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = false,
			ValidIssuer = $"{authService.Host}/realms/{authService.Realm}",
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("abcdefghi12345"))
		};

    });

builder.Services.AddCors(b =>
{
	b.AddPolicy("default", policy =>
	{
		policy.AllowAnyMethod()
			  .AllowAnyOrigin()
			  .AllowAnyHeader();
	});
});

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("admin", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("POWER-USER");
    });    
});

// Add Swagger services
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();
// Seed the database
await DbInitializer.SeedData(app);
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("default");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();