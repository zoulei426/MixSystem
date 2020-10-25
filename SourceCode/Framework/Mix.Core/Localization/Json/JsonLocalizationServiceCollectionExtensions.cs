using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;
using Mix.Core;
using Mix.Core.Localization.Json;
using Mix.Core.Localization.Json.Internal;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// JsonLocalizationServiceCollectionExtensions
    /// </summary>
    public static class JsonLocalizationServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the json localization.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">services</exception>
        public static IServiceCollection AddJsonLocalization(this IServiceCollection services)
        {
            Guards.ThrowIfNull(services);

            services.AddOptions();

            AddJsonLocalizationServices(services);

            return services;
        }

        /// <summary>
        /// Adds the json localization.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="setupAction">The setup action.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// services
        /// or
        /// setupAction
        /// </exception>
        public static IServiceCollection AddJsonLocalization(
           this IServiceCollection services,
           Action<JsonLocalizationOptions> setupAction)
        {
            Guards.ThrowIfNull(services);

            Guards.ThrowIfNull(setupAction);

            AddJsonLocalizationServices(services, setupAction);

            return services;
        }

        /// <summary>
        /// Adds the json localization services.
        /// </summary>
        /// <param name="services">The services.</param>
        internal static void AddJsonLocalizationServices(IServiceCollection services)
        {
            services.TryAddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
            services.TryAddTransient(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));
            services.TryAddTransient(typeof(IStringLocalizer), typeof(StringLocalizer));
        }

        /// <summary>
        /// Adds the json localization services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="setupAction">The setup action.</param>
        internal static void AddJsonLocalizationServices(
            IServiceCollection services,
            Action<JsonLocalizationOptions> setupAction)
        {
            AddJsonLocalizationServices(services);
            services.Configure(setupAction);
        }
    }
}