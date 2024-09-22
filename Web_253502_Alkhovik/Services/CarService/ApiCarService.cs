using System.Text;
using System.Text.Json;
using Web_253502_Alkhovik.Domain.Entities;
using Web_253502_Alkhovik.Domain.Models;
using Web_253502_Alkhovik.Services.FileService;

namespace Web_253502_Alkhovik.Services.CarService;

public class ApiCarService : ICarService
{
	private readonly IHttpClientFactory _httpClientFactory;
	private readonly IFileService _fileService;
	private readonly JsonSerializerOptions _serializerOptions;
	private readonly ILogger<ApiCarService> _logger;
	private readonly string _pageSize;
	public ApiCarService(IConfiguration configuration, 
								IHttpClientFactory httpClientFactory, 
								IFileService fileService,
								ILogger<ApiCarService> logger)
	{
		_httpClientFactory = httpClientFactory;
		_fileService = fileService;
		_serializerOptions = new JsonSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};
		_logger = logger;
		_pageSize = configuration.GetSection("ItemsPerPage").Value;
	}

	public async Task<ResponseData<Car>> CreateProductAsync(Car car, IFormFile? formFile)
	{
		car.Image = "images/noimage.jpg";

		if (formFile != null)
		{
			var imageUrl = await _fileService.SaveFileAsync(formFile);

			if (!string.IsNullOrEmpty(imageUrl))
				car.Image = imageUrl;
		}

		var client = _httpClientFactory.CreateClient("api");
			
		var uri = new Uri(client.BaseAddress.AbsoluteUri + "car");
		
		var response = await client.PostAsJsonAsync(uri, car, _serializerOptions);
		if (!response.IsSuccessStatusCode)
		{
			_logger.LogError($"-----> object not created. Error:{ response.StatusCode.ToString()}");
			return ResponseData<Car>.Error($"Object not added. Error:{response.StatusCode.ToString()}");
		}

		return await response.Content.ReadFromJsonAsync<ResponseData<Car>>(_serializerOptions);
	}

	public async Task DeleteProductAsync(int id)
	{
		var client = _httpClientFactory.CreateClient("api");

		var response = await client.DeleteAsync($"{client.BaseAddress}car/{id}");
		if (!response.IsSuccessStatusCode)
		{
			throw new Exception("Delete operation failed");
		}
		return;
	}

	public async Task<ResponseData<Car>> GetProductByIdAsync(int id)
	{
		var client = _httpClientFactory.CreateClient("api");

		var response = await client.GetAsync($"{client.BaseAddress}car/{id}");
		if (!response.IsSuccessStatusCode)
		{
			return ResponseData<Car>.Error($"Error: {response.StatusCode.ToString()}");
		}

		var product = await response.Content.ReadFromJsonAsync<Car>();

		return ResponseData<Car>.Success(product);
	}

	public async Task<ResponseData<ProductListModel<Car>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
	{
		var client = _httpClientFactory.CreateClient("api");

		var urlString = new StringBuilder($"{client.BaseAddress.AbsoluteUri}car");

		if (categoryNormalizedName != null)
		{
			urlString.Append($"/{categoryNormalizedName}/");
		}
		if (pageNo >= 1) 
		{
			urlString.Append($"?pageNo={pageNo}");
		}
		if (!_pageSize.Equals("3"))
		{
			urlString.Append(QueryString.Create("&pageSize", _pageSize));
		}

		var response = await client.GetAsync(new Uri(urlString.ToString()));
		
		if (response.IsSuccessStatusCode)
		{
			try
			{
				return await response
				.Content
				.ReadFromJsonAsync<ResponseData<ProductListModel<Car>>>
															(_serializerOptions);
			}
			catch (JsonException ex)
			{
				_logger.LogError($"-----> Error: {ex.Message}");
				return ResponseData<ProductListModel<Car>>.Error($"Error: {ex.Message}");
			}
		}

		_logger.LogError($"-----> Error in fetching data from server. Error:{ response.StatusCode.ToString()}");

		return ResponseData<ProductListModel<Car>>.Error($"Data not fetched. Error:{response.StatusCode.ToString()}");
	}

	public async Task UpdateProductAsync(int id, Car car, IFormFile? formFile)
	{
		var client = _httpClientFactory.CreateClient("api");

		if (formFile != null)
		{
			try
			{
				await _fileService.DeleteFileAsync(car.Image);
			}
			catch (Exception ex)
			{
				throw;
			}

			var imageUrl = await _fileService.SaveFileAsync(formFile);

			if (!string.IsNullOrEmpty(imageUrl))
				car.Image = imageUrl;
		}

		// TODO var response = await client.PutAsJsonAsync<
	}
}