using Web_253502_Alkhovik.Services.CarService;
using Web_253502_Alkhovik.Services.CategoryService;
using Web_253502_Alkhovik.Helpers;
using Web_253502_Alkhovik.Services.Authentication;
using Web_253502_Alkhovik.Services.FileService;

namespace Web_253502_Alkhovik.Extensions
{
    public static class HostingExtensions
    {
        public static void RegisterCustomServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpClient<ITokenAccessor, KeycloakTokenAccessor>();
            builder.Services.AddHttpClient<IAuthService, KeycloakAuthService>();
            builder.Services.AddHttpClient<ICategoryService, ApiCategoryService>("api");
            builder.Services.AddHttpClient<ICarService, ApiCarService>("api");
            builder.Services.AddHttpClient<IFileService, ApiFileService>("filesapi");
            builder.Services.Configure<KeycloakData>(builder.Configuration.GetSection("keycloak"));
        }
    }
}