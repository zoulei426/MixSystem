using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Mix.Core;
using System;

namespace Mix.Service.Core.Attributes
{
    /// <summary>
    /// RequestHeaderMatchesMediaTypeAttribute
    /// </summary>
    /// <seealso cref="System.Attribute" />
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ActionConstraints.IActionConstraint" />
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
    public class RequestHeaderMatchesMediaTypeAttribute : Attribute, IActionConstraint
    {
        private readonly MediaTypeCollection mediaTypes = new MediaTypeCollection();
        private readonly string requestHeaderToMatch;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestHeaderMatchesMediaTypeAttribute"/> class.
        /// </summary>
        /// <param name="requestHeaderToMatch">The request header to match.</param>
        /// <param name="mediaType">Type of the media.</param>
        /// <param name="otherMediaTypes">The other media types.</param>
        public RequestHeaderMatchesMediaTypeAttribute(string requestHeaderToMatch,
            string mediaType, params string[] otherMediaTypes)
        {
            Guards.ThrowIfNull(requestHeaderToMatch);
            this.requestHeaderToMatch = requestHeaderToMatch;

            if (MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue mediaTypeHeaderValue))
            {
                mediaTypes.Add(mediaTypeHeaderValue);
            }
            else
            {
                throw new ArgumentException(null, nameof(mediaType));
            }

            foreach (var otherMediaType in otherMediaTypes)
            {
                if (MediaTypeHeaderValue.TryParse(otherMediaType, out MediaTypeHeaderValue otherMediaTypeHeaderValue))
                {
                    mediaTypes.Add(otherMediaTypeHeaderValue);
                }
                else
                {
                    throw new ArgumentException(null, nameof(otherMediaTypes));
                }
            }
        }

        /// <summary>
        /// Determines whether an action is a valid candidate for selection.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.ActionConstraints.ActionConstraintContext" />.</param>
        /// <returns>
        /// True if the action is valid for selection, otherwise false.
        /// </returns>
        public bool Accept(ActionConstraintContext context)
        {
            var requestHeaders = context.RouteContext.HttpContext.Request.Headers;
            if (!requestHeaders.ContainsKey(requestHeaderToMatch))
            {
                return false;
            }

            var requestMediaType = new MediaType(requestHeaders[requestHeaderToMatch]);

            foreach (var mediaType in mediaTypes)
            {
                var parsedMediaType = new MediaType(mediaType);
                if (requestMediaType.Equals(parsedMediaType))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// The constraint order.
        /// </summary>
        /// <remarks>
        /// Constraints are grouped into stages by the value of <see cref="P:Microsoft.AspNetCore.Mvc.ActionConstraints.IActionConstraint.Order" />. See remarks on
        /// <see cref="T:Microsoft.AspNetCore.Mvc.ActionConstraints.IActionConstraint" />.
        /// </remarks>
        public int Order => 0;
    }
}