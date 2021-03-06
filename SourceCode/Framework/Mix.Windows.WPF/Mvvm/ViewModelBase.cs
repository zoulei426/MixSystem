﻿using Microsoft.Extensions.Localization;
using Mix.Core;
using Mix.Core.Loggers;
using Mix.Core.Notify;
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
        private readonly IStringLocalizer _Localizer;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the dispatcher.
        /// </summary>
        /// <value>
        /// The dispatcher.
        /// </value>
        public Dispatcher Dispatcher { get; set; }

        /// <summary>
        /// 日志对象
        /// </summary>
        //protected ILogger Logger { get; }

        protected IStringLocalizer Localizer => _Localizer;

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

        /// <summary>
        /// 配置文件
        /// </summary>
        protected IConfigureFile ConfigureFile { get; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        protected ILogger Logger { get; }

        /// <summary>
        /// Gets the notifier.
        /// </summary>
        /// <value>
        /// The notifier.
        /// </value>
        protected INotifier Notifier { get; }

        #endregion Properties

        #region Ctor

        /// <summary>
        /// 基类ViewModel构造函数
        /// </summary>
        /// <param name="container">注入容器</param>
        public ViewModelBase(IContainerExtension container)
        {
            Container = container;
            var fac = container.Resolve<IStringLocalizerFactory>();
            _Localizer = fac.Create("", "");
            _DialogService = container.Resolve<IDialogService>();
            _RegionManager = container.Resolve<IRegionManager>();
            EventAggregator = container.Resolve<IEventAggregator>();
            ConfigureFile = container.Resolve<IConfigureFile>();
            Logger = container.Resolve<ILogger>();
            Notifier = container.Resolve<INotifier>();

            RegisterCommands();
            SubscribeEvents();
        }

        #endregion Ctor

        #region Methods

        /// <summary>
        /// Invokes the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        protected virtual void Invoke(Action action) => Dispatcher.Invoke(action);

        /// <summary>
        /// 注册命令
        /// </summary>
        protected virtual void RegisterCommands()
        {
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        protected virtual void SubscribeEvents()
        {
        }

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

        #endregion Methods
    }
}