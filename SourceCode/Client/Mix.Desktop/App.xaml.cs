using Mix.Windows.Core;
using Prism.Ioc;
using Prism.Logging;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Mix.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        #region Methods

        #region Methods - Override

        /// <summary>
        /// 创建窗体
        /// </summary>
        /// <returns></returns>
        protected override Window CreateShell()
        {
            InitializeCultureInfo();
            return Container.Resolve<LoginWindow>();
        }

       

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //UI线程未捕获异常处理事件
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            //非UI线程未捕获异常处理事件
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            //Task线程内未捕获异常处理事件
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ILoggerFacade, Logger>();
            containerRegistry.RegisterInstance(new ConfigureFile().Load());
        }
        #endregion

        #region Methods - Private
        private void InitializeCultureInfo()
        {
            var configure = Container.Resolve<IConfigureFile>();

            I18nManager.Initialize(configure);
            var language = configure.GetValue<CultureInfo>("language");
            if (language == null)
            {
                language = CultureInfo.InstalledUICulture;
                configure.SetValue("language", language);
            }

            I18nManager.Instance.CurrentUICulture = language;
            //I18nManager.Instance.AddResourceManager(UiResources.ResourceManager);
        }

        /// <summary>
        /// UI线程未捕获异常处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 非UI线程未捕获异常处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Task线程内未捕获异常处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            //task线程内未处理捕获
            //MessageBoxHelper.Exception(e.Exception, SystemInfo.CATCH_THREAD_UNHANDLED_EXCEPTION);
            //设置该异常已察觉（这样处理后就不会引起程序崩溃）
            e.SetObserved();
        }
        #endregion

        #endregion


        




       
    }
}
