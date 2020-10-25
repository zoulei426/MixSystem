using Microsoft.Extensions.Logging;
using System;
using System.Globalization;

namespace Mix.Core.Localization.Json.Internal
{
    /// <summary>
    /// JsonStringLocalizerLoggerExtensions
    /// </summary>
    internal static class JsonStringLocalizerLoggerExtensions
    {
        /// <summary>
        /// The searched location
        /// </summary>
        private static readonly Action<ILogger, string, string, CultureInfo, Exception> _searchedLocation;

        /// <summary>
        /// Initializes the <see cref="JsonStringLocalizerLoggerExtensions"/> class.
        /// </summary>
        static JsonStringLocalizerLoggerExtensions()
        {
            _searchedLocation = LoggerMessage.Define<string, string, CultureInfo>(
                LogLevel.Debug,
                1,
                $"{nameof(JsonStringLocalizer)} searched for '{{Key}}' in '{{LocationSearched}}' with culture '{{Culture}}'.");
        }

        /// <summary>
        /// Searcheds the location.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="key">The key.</param>
        /// <param name="searchedLocation">The searched location.</param>
        /// <param name="culture">The culture.</param>
        public static void SearchedLocation(this ILogger logger, string key, string searchedLocation, CultureInfo culture)
        {
            _searchedLocation(logger, key, searchedLocation, culture, null);
        }
    }
}