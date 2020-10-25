using AutoMapper;
using FreeSql;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;

namespace Mix.Data.Services
{
    /// <summary>
    /// 应用层服务
    /// </summary>
    /// <seealso cref="Mix.Data.Services.IApplicationService" />
    public abstract class ApplicationService : IApplicationService
    {
        #region Fields

        private ICurrentUser _currentUser;
        private IMapper _mapper;
        private UnitOfWorkManager unitOfWorkManager;
        private ILoggerFactory _loggerFactory;
        private Lazy<ILogger> _lazyLogger => new Lazy<ILogger>(() => LoggerFactory?.CreateLogger(GetType().FullName) ?? NullLogger.Instance, true);
        private IAuthorizationService _authorizationService;

        #endregion Fields

        /// <summary>
        /// Gets or sets the service provider.
        /// </summary>
        /// <value>
        /// The service provider.
        /// </value>
        public IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// The service provider lock
        /// </summary>
        protected readonly object ServiceProviderLock = new object();

        /// <summary>
        /// Lazies the get required service.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="reference">The reference.</param>
        /// <returns></returns>
        protected TService LazyGetRequiredService<TService>(ref TService reference)
            => LazyGetRequiredService(typeof(TService), ref reference);

        /// <summary>
        /// Lazies the get required service.
        /// </summary>
        /// <typeparam name="TRef">The type of the reference.</typeparam>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="reference">The reference.</param>
        /// <returns></returns>
        protected TRef LazyGetRequiredService<TRef>(Type serviceType, ref TRef reference)
        {
            if (reference == null)
            {
                lock (ServiceProviderLock)
                {
                    if (reference == null)
                    {
                        reference = (TRef)ServiceProvider.GetRequiredService(serviceType);
                    }
                }
            }

            return reference;
        }

        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        public ICurrentUser CurrentUser => LazyGetRequiredService(ref _currentUser);

        /// <summary>
        /// Gets the mapper.
        /// </summary>
        /// <value>
        /// The mapper.
        /// </value>
        public IMapper Mapper => LazyGetRequiredService(ref _mapper);

        /// <summary>
        /// Gets the unit of work manager.
        /// </summary>
        /// <value>
        /// The unit of work manager.
        /// </value>
        public UnitOfWorkManager UnitOfWorkManager => LazyGetRequiredService(ref unitOfWorkManager);

        /// <summary>
        /// Gets the logger factory.
        /// </summary>
        /// <value>
        /// The logger factory.
        /// </value>
        public ILoggerFactory LoggerFactory => LazyGetRequiredService(ref _loggerFactory);

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        protected ILogger Logger => _lazyLogger.Value;

        /// <summary>
        /// Gets the authorization service.
        /// </summary>
        /// <value>
        /// The authorization service.
        /// </value>
        public IAuthorizationService AuthorizationService => LazyGetRequiredService(ref _authorizationService);
    }
}