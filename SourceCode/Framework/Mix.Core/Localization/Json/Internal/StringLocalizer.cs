using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Globalization;

namespace Mix.Core.Localization.Json.Internal
{
    /// <summary>
    /// StringLocalizer
    /// </summary>
    /// <seealso cref="Microsoft.Extensions.Localization.IStringLocalizer" />
    public class StringLocalizer : IStringLocalizer
    {
        /// <summary>
        /// The localizer
        /// </summary>
        private IStringLocalizer _localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringLocalizer"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public StringLocalizer(IStringLocalizerFactory factory)
        {
            _localizer = factory.Create(string.Empty, PathHelpers.GetApplicationRoot());
        }

        /// <summary>
        /// Gets the <see cref="LocalizedString"/> with the specified name.
        /// </summary>
        /// <value>
        /// The <see cref="LocalizedString"/>.
        /// </value>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public LocalizedString this[string name] => _localizer[name];

        /// <summary>
        /// Gets the <see cref="LocalizedString"/> with the specified name.
        /// </summary>
        /// <value>
        /// The <see cref="LocalizedString"/>.
        /// </value>
        /// <param name="name">The name.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        public LocalizedString this[string name, params object[] arguments] => _localizer[name, arguments];

        /// <summary>
        /// Gets all string resources.
        /// </summary>
        /// <param name="includeParentCultures">A <see cref="T:System.Boolean" /> indicating whether to include strings from parent cultures.</param>
        /// <returns>
        /// The strings.
        /// </returns>
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) => _localizer.GetAllStrings(includeParentCultures);

        /// <summary>
        /// Creates a new <see cref="T:Microsoft.Extensions.Localization.IStringLocalizer" /> for a specific <see cref="T:System.Globalization.CultureInfo" />.
        /// </summary>
        /// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use.</param>
        /// <returns>
        /// A culture-specific <see cref="T:Microsoft.Extensions.Localization.IStringLocalizer" />.
        /// </returns>
        [Obsolete]
        public IStringLocalizer WithCulture(CultureInfo culture) => _localizer.WithCulture(culture);
    }
}