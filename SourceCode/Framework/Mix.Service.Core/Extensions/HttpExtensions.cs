using Microsoft.AspNetCore.Http;

namespace Mix.Service.Core.Extensions
{
    /// <summary>
    /// HttpExtensions
    /// </summary>
    public static class HttpExtensions
    {
        /// <summary>
        /// Gets the request.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <returns></returns>
        public static string GetRequest(this HttpContext httpContext)
        {
            return httpContext.Request.Method + " " + httpContext.Request.Path;
        }
    }
}