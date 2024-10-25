using Web_253502_Alkhovik.Domain.Models;
using Web_253502_Alkhovik.Domain.Entities;

namespace Web_253502_Alkhovik.Services.CategoryService
{
	public class MemoryCategoryService : ICategoryService
	{
		public Task<ResponseData<List<Category>>> GetCategoryListAsync()
		{
			List<Category> carCategories = new List<Category>
			{
				new Category { Name = "Audi", NormalizedName = "audi" },
				new Category { Name = "BMW", NormalizedName = "bmw" },
				new Category { Name = "Mercedes", NormalizedName = "mercedes" },
				new Category { Name = "Volkswagen", NormalizedName = "volkswagen" },
				new Category { Name = "Toyota", NormalizedName = "toyota" },
				new Category { Name = "Ford", NormalizedName = "ford" }
			};

			var result = ResponseData<List<Category>>.Success(carCategories);

			return Task.FromResult(result);
		}
	}
}