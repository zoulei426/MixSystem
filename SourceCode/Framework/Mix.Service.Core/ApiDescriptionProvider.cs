using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Mix.Core;
using System.Linq;

namespace Mix.Service.Core
{
    /// <summary>
    /// ApiDescriptionProvider
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ApiExplorer.IApiDescriptionProvider" />
    public class ApiDescriptionProvider : IApiDescriptionProvider
    {
        /// <summary>
        /// Gets the order value for determining the order of execution of providers. Providers execute in
        /// ascending numeric value of the <see cref="P:Microsoft.AspNetCore.Mvc.ApiExplorer.IApiDescriptionProvider.Order" /> property.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Providers are executed in an ordering determined by an ascending sort of the <see cref="P:Microsoft.AspNetCore.Mvc.ApiExplorer.IApiDescriptionProvider.Order" /> property.
        /// A provider with a lower numeric value of <see cref="P:Microsoft.AspNetCore.Mvc.ApiExplorer.IApiDescriptionProvider.Order" /> will have its
        /// <see cref="M:Microsoft.AspNetCore.Mvc.ApiExplorer.IApiDescriptionProvider.OnProvidersExecuting(Microsoft.AspNetCore.Mvc.ApiExplorer.ApiDescriptionProviderContext)" /> called before that of a provider with a higher numeric value of
        /// <see cref="P:Microsoft.AspNetCore.Mvc.ApiExplorer.IApiDescriptionProvider.Order" />. The <see cref="M:Microsoft.AspNetCore.Mvc.ApiExplorer.IApiDescriptionProvider.OnProvidersExecuted(Microsoft.AspNetCore.Mvc.ApiExplorer.ApiDescriptionProviderContext)" /> method is called in the reverse ordering after
        /// all calls to <see cref="M:Microsoft.AspNetCore.Mvc.ApiExplorer.IApiDescriptionProvider.OnProvidersExecuting(Microsoft.AspNetCore.Mvc.ApiExplorer.ApiDescriptionProviderContext)" />. A provider with a lower numeric value of
        /// <see cref="P:Microsoft.AspNetCore.Mvc.ApiExplorer.IApiDescriptionProvider.Order" /> will have its <see cref="M:Microsoft.AspNetCore.Mvc.ApiExplorer.IApiDescriptionProvider.OnProvidersExecuted(Microsoft.AspNetCore.Mvc.ApiExplorer.ApiDescriptionProviderContext)" /> method called after that of a provider
        /// with a higher numeric value of <see cref="P:Microsoft.AspNetCore.Mvc.ApiExplorer.IApiDescriptionProvider.Order" />.
        /// </para>
        /// <para>
        /// If two providers have the same numeric value of <see cref="P:Microsoft.AspNetCore.Mvc.ApiExplorer.IApiDescriptionProvider.Order" />, then their relative execution order
        /// is undefined.
        /// </para>
        /// </remarks>
        public int Order => 1;

        /// <summary>
        /// Called after <see cref="T:Microsoft.AspNetCore.Mvc.ApiExplorer.IApiDescriptionProvider" /> implementations with higher <see cref="P:Microsoft.AspNetCore.Mvc.ApiExplorer.IApiDescriptionProvider.Order" /> values have been called.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.ApiExplorer.ApiDescriptionProviderContext" />.</param>
        public void OnProvidersExecuted(ApiDescriptionProviderContext context)
        {
        }

        /// <summary>
        /// Creates or modifies <see cref="T:Microsoft.AspNetCore.Mvc.ApiExplorer.ApiDescription" />s.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.ApiExplorer.ApiDescriptionProviderContext" />.</param>
        public void OnProvidersExecuting(ApiDescriptionProviderContext context)
        {
            foreach (ApiParameterDescription parameter in context.Results.SelectMany(x => x.ParameterDescriptions).Where(x => x.Source.Id == "Query" || x.Source.Id == "ModelBinding"))
            {
                parameter.Name = parameter.Name.ToSnakeCase();
            }
        }
    }
}