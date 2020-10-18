using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Core.Localization.Json.Internal
{
    internal static class JsonStringLocalizerLoggerExtensions
    {
        private static readonly Action<ILogger, string, string, CultureInfo, Exception> _searchedLocation;

        static JsonStringLocalizerLoggerExtensions()
        {
            _searchedLocation = LoggerMessage.Define<string, string, CultureInfo>(
                LogLevel.Debug,
                1,
                $"{nameof(JsonStringLocalizer)} searched for '{{Key}}' in '{{LocationSearched}}' with culture '{{Culture}}'.");
        }

        public static void SearchedLocation(this ILogger logger, string key, string searchedLocation, CultureInfo culture)
        {
            _searchedLocation(logger, key, searchedLocation, culture, null);
        }
    }
}