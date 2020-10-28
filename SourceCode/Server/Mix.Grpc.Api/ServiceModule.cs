using Autofac;

namespace Mix.Grpc.Api
{
    /// <summary>
    /// 注入Application层中的Service
    /// </summary>
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            string[] notIncludes = new string[] { };

            //Assembly servicesDllFile = Assembly.Load("LinCms.Application");
            //builder.RegisterAssemblyTypes(servicesDllFile)
            //    .Where(a => a.Name.EndsWith("Service") && !notIncludes.Where(r => r == a.Name).Any())
            //    .AsImplementedInterfaces()
            //    .InstancePerLifetimeScope()
            //    .PropertiesAutowired();// 属性注入

            //一个接口多个实现，使用Named，区分
        }
    }
}