namespace Web_253502_Alkhovik.Services.Authentication
{
    public interface ITokenAccessor
    {
        Task<string> GetAccessTokenAsync();
        Task SetAuthorizationHeaderAsync(HttpClient httpClient);
    }
}