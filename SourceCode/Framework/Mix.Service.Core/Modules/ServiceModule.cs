using Autofac;
using Autofac.Extras.DynamicProxy;
using Mix.Service.Core.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mix.Service.Core.Modules
{
    /// <summary>
    /// 注入Application层中的Service
    /// </summary>
    public class ServiceModule : Autofac.Module
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWorkInterceptor>();
            builder.RegisterType<UnitOfWorkAsyncInterceptor>();

            List<Type> interceptorServiceTypes = new List<Type>()
            {
                typeof(UnitOfWorkInterceptor)
            };

            string[] notIncludes = new string[]
            {
            };

            Assembly servicesDllFile = Assembly.Load("Mix.Library.Services");
            builder.RegisterAssemblyTypes(servicesDllFile)
                .Where(a => a.Name.EndsWith("Service") && !notIncludes.Where(r => r == a.Name).Any())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .PropertiesAutowired()// 属性注入
                .InterceptedBy(interceptorServiceTypes.ToArray())
                .EnableInterfaceInterceptors();

            //一个接口多个实现，使用Named，区分
        }
    }
}