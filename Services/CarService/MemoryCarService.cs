using Web_253502_Alkhovik.Domain.Models;
using Web_253502_Alkhovik.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Web_253502_Alkhovik.Services.CategoryService;

namespace Web_253502_Alkhovik.Services.CarService
{
    public class MemoryCarService : ICarService
    {
        private readonly ICategoryService _categoryService;
        private readonly IConfiguration _config;
        private List<Car> _cars;
        private List<Category> _categories;
        public MemoryCarService([FromServices] IConfiguration config,
                                        ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _categories = _categoryService.GetCategoryListAsync().Result.Data;
            _config = config;

            SetupData();
        }
        public Task<ResponseData<Car>> CreateProductAsync(Car car, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Car>> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<ProductListModel<Car>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            /*		if (string.IsNullOrEmpty(categoryNormalizedName))
                    {
                        return Task.FromResult(ResponseData<ProductListModel<Constructor>>.Error("Empty or null categoryName"));
                    }
            */
            var itemsPerPage = _config.GetValue<int>("ItemsPerPage");
            var items = _cars
                .Where(p => categoryNormalizedName == null || p.Category.NormalizedName.Equals(categoryNormalizedName))
                .ToList();
            int totalPages = (int)Math.Ceiling((double)items.Count() / itemsPerPage);

            var pagedItems = new ProductListModel<Car>
            {
                Items = items.Skip((pageNo - 1) * itemsPerPage).Take(itemsPerPage).ToList(),
                CurrentPage = pageNo,
                TotalPages = totalPages
            };

            var result = ResponseData<ProductListModel<Car>>.Success(pagedItems);

            return Task.FromResult(result);
        }

        public Task UpdateProductAsync(int id, Car car, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        private void SetupData()
        {
            _cars = new List<Car>
            {
                new Car
                {
                    Id = 1,
                    Amount = 100,
                    Category = _categories[0],
                    CategoryId = 1,
                    Description = "Audi A6",
                    Image = "images/audi.jpg",
                    Price = 9.99m
                },
                
                new Car
                {
                    Id = 2,
                    Amount = 100,
                    Category = _categories[1],
                    CategoryId = 2,
                    Description = "BMW X5",
                    Image = "images/bmw.jpg",
                    Price = 19.99m
                },

                new Car
                {
                    Id = 3,
                    Amount = 100,
                    Category = _categories[2],
                    CategoryId = 3,
                    Description = "Mercedes-Benz S-Class",
                    Image = "images/mercedes.jpg",
                    Price = 29.99m
                },

                new Car
                {
                    Id = 4,
                    Amount = 100,
                    Category = _categories[3],
                    CategoryId = 4,
                    Description = "Volkswagen Golf",
                    Image = "images/vw.jpg",
                    Price = 39.99m
                },

                new Car
                {
                    Id = 5,
                    Amount = 100,
                    Category = _categories[4],
                    CategoryId = 5,
                    Description = "Toyota Camry",
                    Image = "images/toyota.jpg",
                    Price = 49.99m
                },

                new Car
                {
                    Id = 6,
                    Amount = 100,
                    Category = _categories[5],
                    CategoryId = 6,
                    Description = "Ford Mustang",
                    Image = "images/ford.jpg",
                    Price = 59.99m
                }
            };
        }   
    }
}