using Web_253502_Alkhovik.Domain.Entities;

namespace Web_253502_Alkhovik.API.Data;

public static class DbInitializer
{
	public static async Task SeedData(WebApplication app)
	{
		var baseUrl = app.Configuration.GetSection("BaseUrl").Value;

		List<Category> _categories = new List<Category>
		{
			new Category { Name = "Audi", NormalizedName = "audi" },
            new Category { Name = "BMW", NormalizedName = "bmw" },
            new Category { Name = "Mercedes-Benz", NormalizedName = "mercedes" },
            new Category { Name = "Volkswagen", NormalizedName = "volkswagen" },
            new Category { Name = "Toyota", NormalizedName = "toyota" },
            new Category { Name = "Ford", NormalizedName = "ford" },
		};

		List<Car> Cars = new List<Car>
		{
			new Car
            {
                Amount = 100,
                Category = _categories[0],
                Description = "Audi A6",
                Image = $"{baseUrl}/images/audi.jpg",
                Price = 9.99m
            },
            new Car
            {
                Amount = 100,
                Category = _categories[1],
                Description = "BMW X5",
                Image = $"{baseUrl}/images/bmw.jpg",
                Price = 19.99m
            },
            new Car
            {
                Amount = 100,
                Category = _categories[2],
                Description = "Mercedes-Benz S-Class",
                Image = $"{baseUrl}/images/mercedes.jpg",
                Price = 29.99m
            },
            new Car
            {
                Amount = 100,
                Category = _categories[3],
                Description = "Volkswagen Golf",
                Image = $"{baseUrl}/images/vw.jpg",
                Price = 39.99m
            },
            new Car
            {
                Amount = 100,
                Category = _categories[4],
                Description = "Toyota Camry",
                Image = $"{baseUrl}/images/toyota.jpg",
                Price = 49.99m
            },
            new Car
            {
                Amount = 100,
                Category = _categories[5],
                Description = "Ford Mustang",
                Image = $"{baseUrl}/images/ford.jpg",
                Price = 59.99m
            }
		};

		using var scope = app.Services.CreateScope();

		var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

		await context.Categories.AddRangeAsync(_categories);
		await context.Cars.AddRangeAsync(Cars);

		await context.SaveChangesAsync();
	}
}