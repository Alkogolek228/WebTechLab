using Web_253502_Alkhovik.Domain.Entities;
using Web_253502_Alkhovik.Domain.Models;

namespace Web_253502_Alkhovik.API.Services.CarService;

public interface ICarService
{
	public Task<ResponseData<ProductListModel<Car>>> GetProductListAsync(string?
		categoryNormalizedName, int pageNo = 1, int pageSize = 3);
	public Task<ResponseData<Car>> GetProductByIdAsync(int id);
	public Task UpdateProductAsync(int id, Car car, IFormFile? formFile);
	public Task DeleteProductAsync(int id);
	public Task<ResponseData<Car>> CreateProductAsync(Car car, IFormFile? formFile);
	public Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile);
}