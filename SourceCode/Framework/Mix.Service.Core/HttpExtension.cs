using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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