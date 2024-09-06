using Web_253502_Alkhovik.Domain.Models;
using Web_253502_Alkhovik.Domain.Entities;

namespace Web_253502_Alkhovik.Services.CarService
{
    public interface ICarService
    {
        public Task<ResponseData<ProductListModel<Car>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1);
        public Task<ResponseData<Car>> GetProductByIdAsync(int id);
        public Task UpdateProductAsync(int id, Car car, IFormFile? formFile);
        public Task DeleteProductAsync(int id);
        public Task<ResponseData<Car>> CreateProductAsync(Car car, IFormFile? formFile);
    }
}