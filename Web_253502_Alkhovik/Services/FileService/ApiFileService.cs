using Web_253502_Alkhovik.Services.Authentication;

namespace Web_253502_Alkhovik.Services.FileService
{
    public class ApiFileService : IFileService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenAccessor _tokenAccessor;
        public ApiFileService(HttpClient httpClient, ITokenAccessor tokenAccessor)
        {
            _httpClient = httpClient;
            _tokenAccessor = tokenAccessor;
        }
        public async Task DeleteFileAsync(string fileName)
        {
            await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);

            var response = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}/{fileName}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Cannot delete file");
            }
        }

        public async Task<string> SaveFileAsync(IFormFile formFile)
        {

            await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);

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

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return string.Empty;
        }
    }
}