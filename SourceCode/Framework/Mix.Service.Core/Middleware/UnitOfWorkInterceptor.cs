using Castle.DynamicProxy;

namespace Mix.Service.Core.Middleware
{
    /// <summary>
    /// UnitOfWorkInterceptor
    /// </summary>
    /// <seealso cref="Castle.DynamicProxy.IInterceptor" />
    public class UnitOfWorkInterceptor : IInterceptor
    {
        private readonly UnitOfWorkAsyncInterceptor asyncInterceptor;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWorkInterceptor"/> class.
        /// </summary>
        /// <param name="interceptor">The interceptor.</param>
        public UnitOfWorkInterceptor(UnitOfWorkAsyncInterceptor interceptor)
        {
            asyncInterceptor = interceptor;
        }

        /// <summary>
        /// Intercepts the specified invocation.
        /// </summary>
        /// <param name="invocation">The invocation.</param>
        public void Intercept(IInvocation invocation)
        {
            asyncInterceptor.ToInterceptor().Intercept(invocation);
        }
    }
}