using Microsoft.AspNetCore.Http;

namespace Mix.Service.Core
{
    public static class HttpExtension
    {
        public static string GetRequest(this HttpContext httpContext)
        {
            return httpContext.Request.Method + " " + httpContext.Request.Path;
        }
    }
}