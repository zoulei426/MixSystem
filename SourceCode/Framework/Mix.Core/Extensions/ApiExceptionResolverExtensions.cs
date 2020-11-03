using Mix.Core.Notify;
using Newtonsoft.Json.Linq;
using Prism.Ioc;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Core.Extensions
{
    /// <summary>
    /// ApiExceptionResolverExtensions
    /// </summary>
    public static class ApiExceptionResolverExtensions
    {
        private class ApiExceptionResolver
        {
            private readonly INotifier notifier;

            public ApiExceptionResolver(INotifier notifier)
            {
                this.notifier = notifier;
            }

            public async Task RunApiInternal(Task task, Action onSuccessCallback)
            {
                try
                {
                    await task;
                    onSuccessCallback?.Invoke();
                }
                catch (ApiException e)
                {
                    notifier.Error(JObject.Parse(e.Content).Value<string>("status"));
                }
                catch (HttpRequestException httpRequestException)
                {
                    notifier.Error(httpRequestException.InnerException?.Message);
                }
            }

            public async Task<T> RunApiInternal<T>(Task<T> task)
            {
                try
                {
                    return await task;
                }
                catch (ApiException apiException)
                {
                    var message = JObject.Parse(apiException.Content)["status"].ToString();
                    notifier.Error(message);
                }
                catch (HttpRequestException httpRequestException)
                {
                    notifier.Error(httpRequestException.InnerException?.Message);
                }

                return default(T);
            }
        }

        private static IContainerProvider container;

        /// <summary>
        /// Sets the unity container.
        /// </summary>
        /// <param name="container">The container.</param>
        public static void SetUnityContainer(IContainerProvider container) => ApiExceptionResolverExtensions.container = container;

        /// <summary>
        /// Runs the API.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <param name="onSuccessCallback">The on success callback.</param>
        /// <returns></returns>
        public static Task RunApi(this Task task, Action onSuccessCallback = null) =>
            container.Resolve<ApiExceptionResolver>().RunApiInternal(task, onSuccessCallback);

        /// <summary>
        /// Runs the API.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="task">The task.</param>
        /// <returns></returns>
        public static Task<T> RunApi<T>(this Task<T> task) =>
            container.Resolve<ApiExceptionResolver>().RunApiInternal(task);
    }
}