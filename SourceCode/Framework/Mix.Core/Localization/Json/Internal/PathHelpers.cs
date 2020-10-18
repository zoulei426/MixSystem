using System.IO;
using System.Reflection;

namespace Mix.Core.Localization.Json.Internal
{
    public static class PathHelpers
    {
        public static string GetApplicationRoot()
            => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }
}