﻿using Autofac;
using Mix.Core;
using System;
using System.Linq;
using System.Reflection;

namespace Mix.Service.Core.Modules
{
    /// <summary>
    /// 接口注入
    /// </summary>
    public class DependencyModule : Autofac.Module
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
            Assembly[] currentAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(r => r.FullName.Contains("Mix")).ToArray();

            //每次调用，都会重新实例化对象；每次请求都创建一个新的对象；
            Type transientDependency = typeof(ITransientDependency);
            builder.RegisterAssemblyTypes(currentAssemblies)
                .Where(t => transientDependency.GetTypeInfo().IsAssignableFrom(t) && t.IsClass && !t.IsAbstract && !t.IsGenericType)
                .AsImplementedInterfaces().InstancePerDependency();

            //同一个Lifetime生成的对象是同一个实例
            Type scopeDependency = typeof(IScopedDependency);
            builder.RegisterAssemblyTypes(currentAssemblies)
                .Where(t => scopeDependency.GetTypeInfo().IsAssignableFrom(t) && t.IsClass && !t.IsAbstract && !t.IsGenericType)
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            //单例模式，每次调用，都会使用同一个实例化的对象；每次都用同一个对象；
            Type singletonDependency = typeof(ISingletonDependency);
            builder.RegisterAssemblyTypes(currentAssemblies)
                .Where(t => singletonDependency.GetTypeInfo().IsAssignableFrom(t) && t.IsClass && !t.IsAbstract && !t.IsGenericType)
                .AsImplementedInterfaces().SingleInstance();
        }
    }
}