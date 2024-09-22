using Microsoft.AspNetCore.Http.HttpResults;

namespace Web_253502_Alkhovik.Services.FileService
{
    public class ApiFileService : IFileService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpContext _httpContext;
        public ApiFileService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task DeleteFileAsync(string fileName)
        {
            var client = _httpClientFactory.CreateClient("filesapi");

            var response = await client.DeleteAsync($"{client.BaseAddress}/{fileName}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Cannot delete file");
            }
        }

        public async Task<string> SaveFileAsync(IFormFile formFile)
        {
            var client = _httpClientFactory.CreateClient("filesapi");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post
            };

            var extension = Path.GetExtension(formFile.FileName);
            var newName = Path.ChangeExtension(Path.GetRandomFileName(), extension);

            var content = new MultipartFormDataContent();
            var streamContent = new StreamContent(formFile.OpenReadStream());
            content.Add(streamContent, "file", newName);

            request.Content = content;

            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return string.Empty;
        }
    }
}