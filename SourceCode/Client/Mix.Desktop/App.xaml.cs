using Microsoft.Extensions.Localization;
using Mix.Core;
using Mix.Core.Localization.Json;
using Mix.Windows.Core;
using Mix.Windows.WPF;
using Mix.Windows.WPF.Localizations;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Serilog;
using Serilog.Events;
using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Mix.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        #region Methods

        #region Methods - Override

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Log.Logger = new LoggerConfiguration()
             .MinimumLevel.Information()//最小的记录等级
             .MinimumLevel.Override("Microsoft", LogEventLevel.Information)//对其他日志进行重写,除此之外,目前框架只有微软自带的日志组件
             .WriteTo.File(Path.Combine(SystemPath.Logs, "log.txt"),
                      rollingInterval: RollingInterval.Day)
             .CreateLogger();

            //UI线程未捕获异常处理事件
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            //非UI线程未捕获异常处理事件
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            //Task线程内未捕获异常处理事件
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            Exit += App_Exit;
        }

       

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
            containerRegistry.RegisterInstance(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));
            containerRegistry.RegisterInstance(typeof(IStringLocalizer), typeof(Mix.Core.Localization.Json.Internal.StringLocalizer));

            containerRegistry.RegisterInstance(new ConfigureFile().Load());
        }

        protected override void ConfigureViewModelLocator()
        {
            ViewModelLocationProvider.SetDefaultViewModelFactory(
                new ViewModelResolver(() => Container).UseDefaultConfigure().ResolveViewModelForView);
        }

        /// <summary>
        /// 创建窗体
        /// </summary>
        /// <returns></returns>
        protected override Window CreateShell()
        {
            InitializeCultureInfo();
            return Container.Resolve<LoginWindow>();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog { ModulePath = @".\Modules" };
        }

        #endregion Methods - Override

        #region Methods - Private

        private void InitializeCultureInfo()
        {
            var configure = Container.Resolve<IConfigureFile>();

            LocalizerManager.Initialize(configure, Container.Resolve<IStringLocalizerFactory>());

            var language = configure.GetValue<CultureInfo>(SystemConst.LANGUAGE);
            if (language == null)
            {
                language = CultureInfo.InstalledUICulture;
                configure.SetValue(SystemConst.LANGUAGE, language);
            }

            CultureInfo.CurrentUICulture = language;
            //System.Threading.Thread.CurrentThread.CurrentCulture = language;
            //System.Threading.Thread.CurrentThread.CurrentUICulture = language;

            LocalizerManager.Instance.CurrentUICulture = language;
        }

        /// <summary>
        /// UI线程未捕获异常处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            MessageBox.Show($"程序运行出错，原因：{ex.Message}-{ex.InnerException?.Message}",
                "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
            Log.Error(ex.Message, ex);
            e.Handled = true;
        }

        /// <summary>
        /// 非UI线程未捕获异常处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                MessageBox.Show($"程序组件出错，原因：{ex.Message}",
                    "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                Log.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// Task线程内未捕获异常处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            MessageBox.Show($"执行任务出错，原因：{ex.Message}",
                "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
            Log.Error(ex.Message, ex);
            //设置该异常已察觉
            e.SetObserved();
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            Log.CloseAndFlush();
        }

        #endregion Methods - Private

        #endregion Methods
    }
}