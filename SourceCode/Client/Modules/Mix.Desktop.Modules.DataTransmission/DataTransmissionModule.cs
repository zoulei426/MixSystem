using FreeSql;
using FreeSql.Internal;
using Mix.Data;
using Mix.Desktop.Modules.DataTransmission.Views;
using Mix.Library.Repositories.HouseSites;
using Mix.Library.Services;
using Mix.Windows.WPF;
using Mix.Windows.WPF.Mvvm;
using Prism.Ioc;
using System.Diagnostics;
using Unity;

namespace Mix.Desktop.Modules.DataTransmission
{
    public class DataTransmissionModule : ModuleBase
    {
        public DataTransmissionModule(IUnityContainer container) : base(container)
        {
        }

        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register for container
            containerRegistry.RegisterScoped(typeof(UnitOfWorkManager));
            containerRegistry.RegisterSingleton(typeof(IFreeSql), AddFreeSql);

            containerRegistry.Register(typeof(ICurrentUser), typeof(CurrentUser));
            containerRegistry.Register(typeof(IJcxxRepository), typeof(JcxxRepository));
            containerRegistry.Register(typeof(ICyxxRepository), typeof(CyxxRepository));
            containerRegistry.Register(typeof(INfxxRepository), typeof(NfxxRepository));
            containerRegistry.Register(typeof(IHouseSiteService), typeof(HouseSiteService));

            // Register for region
            RegionManager.RegisterViewWithRegion(SystemRegionNames.MainTabRegion, typeof(DataTransmissionComponent));
        }

        public override void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public static IFreeSql AddFreeSql(IContainerProvider containerProvider)
        {
            return new FreeSqlBuilder()
                   .UseConnectionString(DataType.PostgreSQL, "")
                   .UseNameConvert(NameConvertType.PascalCaseToUnderscoreWithLower)
                   .UseAutoSyncStructure(false)
                   .UseNoneCommandParameter(true)
                   .UseMonitorCommand(cmd =>
                   {
                       Trace.WriteLine(cmd.CommandText + ";");
                   })
                   .Build()
                   .SetDbContextOptions(opt => opt.EnableAddOrUpdateNavigateList = false);//联级保存功能开启（默认为关闭）
        }
    }
}