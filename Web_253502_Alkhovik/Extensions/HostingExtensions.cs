using Web_253502_Alkhovik.Services.CarService;
using Web_253502_Alkhovik.Services.CategoryService;

namespace Web_253502_Alkhovik.Extensions
{
    public static class HostingExtensions
    {
        public static void RegisterCustomServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICategoryService, MemoryCategoryService>();
            builder.Services.AddScoped<ICarService, MemoryCarService>();
            
        }
    }
}