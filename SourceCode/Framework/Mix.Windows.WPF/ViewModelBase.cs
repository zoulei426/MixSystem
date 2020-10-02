using Mix.Core;
using Mix.Windows.Core;
using Prism.Events;
using Prism.Ioc;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Windows.Threading;

namespace Mix.Windows.WPF
{
    /// <summary>
    /// 界面ViewModel基类
    /// </summary>
    public class ViewModelBase : BindableObject
    {
        #region Fields

        private readonly IDialogService _DialogService;
        private readonly IRegionManager _RegionManager;

        #endregion Fields

        #region Ctor

        /// <summary>
        /// 基类ViewModel构造函数
        /// </summary>
        /// <param name="container">注入容器</param>
        public ViewModelBase(IContainerExtension container)
        {
            Container = container;
            _DialogService = container.Resolve<IDialogService>();
            _RegionManager = container.Resolve<IRegionManager>();
            EventAggregator = container.Resolve<IEventAggregator>();
            Logger = container.Resolve<ILogger>();
        }

        #endregion Ctor

        public Dispatcher Dispatcher { get; set; }

        /// <summary>
        /// 日志对象
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// 事件汇总器，用于发布或订阅事件
        /// </summary>
        protected IEventAggregator EventAggregator { get; }

        /// <summary>
        /// 区域管理器
        /// </summary>
        protected IRegionManager RegionManager => _RegionManager;

        /// <summary>
        /// 依赖注入容器
        /// </summary>
        protected IContainerExtension Container { get; }

        protected virtual void Invoke(Action action) => Dispatcher.Invoke(action);

        /// <summary>
        /// 导航到指定Page
        /// </summary>
        /// <param name="regionName">区域名称</param>
        /// <param name="target">目标Page名称</param>
        /// <param name="navigationCallback">导航回调函数</param>
        protected void Navigate(string regionName, string target, Action<NavigationResult> navigationCallback = null)
        {
            IRegion region = _RegionManager.Regions[regionName];
            if (region == null) return;
            region.RemoveAll();
            if (navigationCallback != null)
                region.RequestNavigate(target, navigationCallback);
            else
                region.RequestNavigate(target);
        }

        /// <summary>
        /// 弹框提示
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="callback">回调函数</param>
        protected void Alert(string message, Action<IDialogResult> callback = null)
        {
            _DialogService.ShowDialog("AlertDialog", new DialogParameters($"message={message}"), callback);
        }

        /// <summary>
        /// 确认框提示
        /// </summary>
        /// <param name="message">确认框消息</param>
        /// <param name="callback">回调函数</param>
        protected void Confirm(string message, Action<IDialogResult> callback = null)
        {
            _DialogService.ShowDialog("ConfirmDialog", new DialogParameters($"message={message}"), callback);
        }
    }
}