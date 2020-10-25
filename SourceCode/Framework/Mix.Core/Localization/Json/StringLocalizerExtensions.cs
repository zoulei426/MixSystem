using Microsoft.Extensions.Localization;
using System;
using System.Linq.Expressions;

namespace Mix.Core.Localization.Json
{
    /// <summary>
    /// StringLocalizerExtensions
    /// </summary>
    public static class StringLocalizerExtensions
    {
        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <typeparam name="TResource">The type of the resource.</typeparam>
        /// <param name="stringLocalizer">The string localizer.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns></returns>
        public static LocalizedString GetString<TResource>(
            this IStringLocalizer stringLocalizer,
            Expression<Func<TResource, string>> propertyExpression)
            => stringLocalizer[(propertyExpression.Body as MemberExpression).Member.Name];
    }
}