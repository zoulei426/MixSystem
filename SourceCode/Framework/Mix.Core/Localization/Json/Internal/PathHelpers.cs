using System.IO;
using System.Reflection;

namespace Mix.Core.Localization.Json.Internal
{
    /// <summary>
    /// PathHelpers
    /// </summary>
    public static class PathHelpers
    {
        /// <summary>
        /// Gets the application root.
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationRoot()
            => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }
}