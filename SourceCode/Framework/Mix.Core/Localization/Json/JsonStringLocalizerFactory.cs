using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mix.Core.Localization.Json.Internal;
using System;
using System.IO;
using System.Reflection;

namespace Mix.Core.Localization.Json
{
    /// <summary>
    /// JsonStringLocalizerFactory
    /// </summary>
    /// <seealso cref="Microsoft.Extensions.Localization.IStringLocalizerFactory" />
    public class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly string _resourcesRelativePath;
        private readonly ResourcesType _resourcesType = ResourcesType.TypeBased;
        private readonly ILoggerFactory _loggerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonStringLocalizerFactory"/> class.
        /// </summary>
        public JsonStringLocalizerFactory()
        {
            _resourcesRelativePath = "Resources";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonStringLocalizerFactory"/> class.
        /// </summary>
        /// <param name="localizationOptions">The localization options.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <exception cref="ArgumentNullException">
        /// localizationOptions
        /// or
        /// loggerFactory
        /// </exception>
        public JsonStringLocalizerFactory(
            IOptions<JsonLocalizationOptions> localizationOptions,
            ILoggerFactory loggerFactory)
        {
            if (localizationOptions == null)
            {
                throw new ArgumentNullException(nameof(localizationOptions));
            }

            _resourcesRelativePath = localizationOptions.Value.ResourcesPath ?? string.Empty;
            _resourcesType = localizationOptions.Value.ResourcesType;
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        /// <summary>
        /// Creates an <see cref="T:Microsoft.Extensions.Localization.IStringLocalizer" /> using the <see cref="T:System.Reflection.Assembly" /> and
        /// <see cref="P:System.Type.FullName" /> of the specified <see cref="T:System.Type" />.
        /// </summary>
        /// <param name="resourceSource">The <see cref="T:System.Type" />.</param>
        /// <returns>
        /// The <see cref="T:Microsoft.Extensions.Localization.IStringLocalizer" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">resourceSource</exception>
        public IStringLocalizer Create(Type resourceSource)
        {
            Guards.ThrowIfNull(resourceSource);

            // TODO: Check why an exception happen before the host build
            if (resourceSource.Name == "Controller")
            {
                return CreateJsonStringLocalizer(Path.Combine(PathHelpers.GetApplicationRoot(), GetResourcePath(resourceSource.Assembly)), TryFixInnerClassPath("Controller"));
            }

            var typeInfo = resourceSource.GetTypeInfo();
            var assembly = typeInfo.Assembly;
            var assemblyName = resourceSource.Assembly.GetName().Name;
            var typeName = $"{assemblyName}.{typeInfo.Name}" == typeInfo.FullName
                ? typeInfo.Name
                : typeInfo.FullName.Substring(assemblyName.Length + 1);
            var resourcesPath = Path.Combine(PathHelpers.GetApplicationRoot(), GetResourcePath(assembly));

            typeName = TryFixInnerClassPath(typeName);

            return CreateJsonStringLocalizer(resourcesPath, typeName);
        }

        /// <summary>
        /// Creates an <see cref="T:Microsoft.Extensions.Localization.IStringLocalizer" />.
        /// </summary>
        /// <param name="baseName">The base name of the resource to load strings from.</param>
        /// <param name="location">The location to load resources from.</param>
        /// <returns>
        /// The <see cref="T:Microsoft.Extensions.Localization.IStringLocalizer" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// baseName
        /// or
        /// location
        /// </exception>
        public IStringLocalizer Create(string baseName, string location)
        {
            Guards.ThrowIfNull(baseName);
            Guards.ThrowIfNull(location);

            baseName = TryFixInnerClassPath(baseName);

            //var assemblyName = new AssemblyName(location);
            //var assembly = Assembly.Load(assemblyName);
            var resourcesPath = Path.Combine(PathHelpers.GetApplicationRoot(), GetResourcePath(null));
            string resourceName = null;

            if (_resourcesType == ResourcesType.TypeBased)
            {
                resourceName = TrimPrefix(baseName, location + ".");
            }

            return CreateJsonStringLocalizer(resourcesPath, resourceName);
        }

        /// <summary>
        /// Creates the json string localizer.
        /// </summary>
        /// <param name="resourcesPath">The resources path.</param>
        /// <param name="resourcename">The resourcename.</param>
        /// <returns></returns>
        protected virtual JsonStringLocalizer CreateJsonStringLocalizer(
            string resourcesPath,
            string resourcename)
        {
            //var logger = _loggerFactory.CreateLogger<JsonStringLocalizer>();

            return new JsonStringLocalizer(
                resourcesPath,
                _resourcesType == ResourcesType.TypeBased ? resourcename : null);
        }

        /// <summary>
        /// Gets the resource path.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns></returns>
        private string GetResourcePath(Assembly assembly)
        {
            var resourceLocationAttribute = assembly?.GetCustomAttribute<ResourceLocationAttribute>();

            return resourceLocationAttribute == null
                ? _resourcesRelativePath
                : resourceLocationAttribute.ResourceLocation;
        }

        /// <summary>
        /// Trims the prefix.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="prefix">The prefix.</param>
        /// <returns></returns>
        private static string TrimPrefix(string name, string prefix)
        {
            if (name.StartsWith(prefix, StringComparison.Ordinal))
            {
                return name.Substring(prefix.Length);
            }

            return name;
        }

        /// <summary>
        /// Tries the fix inner class path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        private string TryFixInnerClassPath(string path)
        {
            const char innerClassSeparator = '+';
            var fixedPath = path;

            if (path.Contains(innerClassSeparator.ToString()))
            {
                fixedPath = path.Replace(innerClassSeparator, '.');
            }

            return fixedPath;
        }
    }
}