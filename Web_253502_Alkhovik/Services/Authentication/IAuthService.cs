namespace Web_253502_Alkhovik.Services.Authentication
{
    public interface IAuthService
    {
        Task<(bool Result, string ErrorMessage)> RegisterUserAsync(string email, string password, IFormFile? avatar);
    }
}