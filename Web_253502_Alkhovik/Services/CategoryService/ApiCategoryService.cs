using System.Text;
using System.Text.Json;
using Web_253502_Alkhovik.Domain.Entities;
using Web_253502_Alkhovik.Domain.Models;
using Web_253502_Alkhovik.Services.Authentication;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace Web_253502_Alkhovik.Services.CategoryService;

public class ApiCategoryService : ICategoryService
{
    private readonly HttpClient _httpClient;
    private readonly ITokenAccessor _tokenAccessor;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly ILogger<ApiCategoryService> _logger;
	public ApiCategoryService(HttpClient httpClient,
								ITokenAccessor tokenAccessor, 
								ILogger<ApiCategoryService> logger)
	{
		_httpClient = httpClient;
		_tokenAccessor = tokenAccessor;
		_serializerOptions = new JsonSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};
		_logger = logger;
	}
	public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
	{
		await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);
		var uriString = new StringBuilder($"{_httpClient.BaseAddress.AbsoluteUri}category");

		var response = await _httpClient.GetAsync(new Uri(uriString.ToString()));
		if (!response.IsSuccessStatusCode)
		{
			return ResponseData<List<Category>>.Error($"Error in fetching data: {response.StatusCode.ToString()}");
		}

		try
		{
			return await response.Content.ReadFromJsonAsync<ResponseData<List<Category>>>(_serializerOptions);
		}
		catch (Exception ex)
		{
			_logger.LogError($"-----> Error: {ex.Message}");
			return ResponseData<List<Category>>.Error($"Error: {ex.Message}");
		}
	}
}