using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Mix.Core.Localization.Json
{
    /// <summary>
    /// JsonStringLocalizer
    /// </summary>
    /// <seealso cref="Microsoft.Extensions.Localization.IStringLocalizer" />
    public class JsonStringLocalizer : IStringLocalizer
    {
        private readonly ConcurrentDictionary<string, IEnumerable<KeyValuePair<string, string>>> _resourcesCache = new ConcurrentDictionary<string, IEnumerable<KeyValuePair<string, string>>>();
        private readonly string _resourcesPath;
        private readonly string _resourceName;
        //private readonly ILogger _logger;

        /// <summary>
        /// The searched location
        /// </summary>
        private string _searchedLocation;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonStringLocalizer"/> class.
        /// </summary>
        /// <param name="resourcesPath">The resources path.</param>
        /// <param name="resourceName">Name of the resource.</param>
        /// <exception cref="ArgumentNullException">resourcesPath</exception>
        public JsonStringLocalizer(
            string resourcesPath,
            string resourceName)
        {
            _resourcesPath = resourcesPath ?? throw new ArgumentNullException(nameof(resourcesPath));
            _resourceName = resourceName;
            //_logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets the <see cref="LocalizedString"/> with the specified name.
        /// </summary>
        /// <value>
        /// The <see cref="LocalizedString"/>.
        /// </value>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">name</exception>
        public LocalizedString this[string name]
        {
            get
            {
                Guards.ThrowIfNull(name);

                var value = GetStringSafely(name);

                return new LocalizedString(name, value ?? name, resourceNotFound: value == null, searchedLocation: _searchedLocation);
            }
        }

        /// <summary>
        /// Gets the <see cref="LocalizedString"/> with the specified name.
        /// </summary>
        /// <value>
        /// The <see cref="LocalizedString"/>.
        /// </value>
        /// <param name="name">The name.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">name</exception>
        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                Guards.ThrowIfNull(name);

                var format = GetStringSafely(name);
                var value = string.Format(format ?? name, arguments);

                return new LocalizedString(name, value, resourceNotFound: format == null, searchedLocation: _searchedLocation);
            }
        }

        /// <summary>
        /// Gets all string resources.
        /// </summary>
        /// <param name="includeParentCultures">A <see cref="T:System.Boolean" /> indicating whether to include strings from parent cultures.</param>
        /// <returns>
        /// The strings.
        /// </returns>
        public virtual IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) =>
            GetAllStrings(includeParentCultures, CultureInfo.CurrentUICulture);

        /// <summary>
        /// Creates a new <see cref="T:Microsoft.Extensions.Localization.IStringLocalizer" /> for a specific <see cref="T:System.Globalization.CultureInfo" />.
        /// </summary>
        /// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use.</param>
        /// <returns>
        /// A culture-specific <see cref="T:Microsoft.Extensions.Localization.IStringLocalizer" />.
        /// </returns>
        public IStringLocalizer WithCulture(CultureInfo culture) => this;

        /// <summary>
        /// Gets all strings.
        /// </summary>
        /// <param name="includeParentCultures">if set to <c>true</c> [include parent cultures].</param>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">culture</exception>
        protected virtual IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures, CultureInfo culture)
        {
            if (culture == null)
            {
                throw new ArgumentNullException(nameof(culture));
            }

            var resourceNames = includeParentCultures
                ? GetAllStringsFromCultureHierarchy(culture)
                : GetAllResourceStrings(culture);

            foreach (var name in resourceNames)
            {
                var value = GetStringSafely(name);
                yield return new LocalizedString(name, value ?? name, resourceNotFound: value == null, searchedLocation: _searchedLocation);
            }
        }

        /// <summary>
        /// Gets the string safely.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">name</exception>
        protected string GetStringSafely(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            var culture = CultureInfo.CurrentUICulture;
            string value = null;

            while (culture != culture.Parent)
            {
                BuildResourcesCache(culture.Name);

                if (_resourcesCache.TryGetValue(culture.Name, out IEnumerable<KeyValuePair<string, string>> resources))
                {
                    var resource = resources?.SingleOrDefault(s => s.Key == name);

                    value = resource?.Value ?? null;
                    //_logger.SearchedLocation(name, _searchedLocation, culture);

                    if (value != null)
                    {
                        break;
                    }

                    culture = culture.Parent;
                }
            }

            return value;
        }

        /// <summary>
        /// Gets all strings from culture hierarchy.
        /// </summary>
        /// <param name="startingCulture">The starting culture.</param>
        /// <returns></returns>
        private IEnumerable<string> GetAllStringsFromCultureHierarchy(CultureInfo startingCulture)
        {
            var currentCulture = startingCulture;
            var resourceNames = new HashSet<string>();

            while (currentCulture != currentCulture.Parent)
            {
                var cultureResourceNames = GetAllResourceStrings(currentCulture);

                if (cultureResourceNames != null)
                {
                    foreach (var resourceName in cultureResourceNames)
                    {
                        resourceNames.Add(resourceName);
                    }
                }

                currentCulture = currentCulture.Parent;
            }

            return resourceNames;
        }

        /// <summary>
        /// Gets all resource strings.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        private IEnumerable<string> GetAllResourceStrings(CultureInfo culture)
        {
            BuildResourcesCache(culture.Name);

            if (_resourcesCache.TryGetValue(culture.Name, out IEnumerable<KeyValuePair<string, string>> resources))
            {
                foreach (var resource in resources)
                {
                    yield return resource.Key;
                }
            }
            else
            {
                yield return null;
            }
        }

        private void BuildResourcesCache(string culture)
        {
            _resourcesCache.GetOrAdd(culture, _ =>
            {
                var resourceFile = string.IsNullOrEmpty(_resourceName)
                    ? $"{culture}.json"
                    : $"{_resourceName}.{culture}.json";

                _searchedLocation = Path.Combine(_resourcesPath, resourceFile);

                if (!File.Exists(_searchedLocation))
                {
                    if (resourceFile.Count(r => r == '.') > 1)
                    {
                        var resourceFileWithoutExtension = Path.GetFileNameWithoutExtension(resourceFile);
                        var resourceFileWithoutCulture = resourceFileWithoutExtension.Substring(0, resourceFileWithoutExtension.LastIndexOf('.'));
                        resourceFile = $"{resourceFileWithoutCulture.Replace('.', Path.DirectorySeparatorChar)}.{culture}.json";
                        _searchedLocation = Path.Combine(_resourcesPath, resourceFile);
                    }
                }

                IEnumerable<KeyValuePair<string, string>> value = null;

                if (File.Exists(_searchedLocation))
                {
                    var builder = new ConfigurationBuilder()
                    .SetBasePath(_resourcesPath)
                    .AddJsonFile(resourceFile, optional: false, reloadOnChange: true);

                    var config = builder.Build();
                    value = config.AsEnumerable();
                }

                return value;
            });
        }
    }
}