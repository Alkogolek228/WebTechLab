using Microsoft.AspNetCore.Http;

namespace Web_253502_Alkhovik.Extensions
{
    public static class HttpRequestExtensions
{
    public static bool IsAjaxRequest(this HttpRequest request)
    {
        return request.Headers["X-Requested-With"] == "XMLHttpRequest"; //IsAjaxRequst()? missing
    }
}
}