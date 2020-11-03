using Mix.Core;
using Mix.Desktop.Modules.DataTransmission.Business;
using Mix.Library.Services;
using Mix.Windows.WPF;
using Prism.Commands;
using Prism.Ioc;
using PropertyChanged;
using System.Windows.Input;

namespace Mix.Desktop.Modules.DataTransmission.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class DataTransmissionPanelViewModel : ViewModelBase, IViewLoadedAndUnloadedAware
    {
        private readonly IHouseSiteService houseSiteService;

        #region Properties

        public string InputFile { get; set; }

        #endregion Properties

        #region Commands

        public ICommand TransmitDataCommand { get; set; }

        #endregion Commands

        public DataTransmissionPanelViewModel(IContainerExtension container, IHouseSiteService houseSiteService) : base(container)
        {
            Guards.ThrowIfNull(container, houseSiteService);
            this.houseSiteService = houseSiteService;
        }

        #region Methods

        protected override void RegisterCommands()
        {
            TransmitDataCommand = new DelegateCommand(TransmitData);
        }

        public void OnLoaded()
        {
        }

        public void OnUnloaded()
        {
        }

        private async void TransmitData()
        {
            string fileName = @"D:\Users\zoulei\Desktop\战旗村人口信息（改）.xls";

            var task = new DataImportTask();
            task.houseSiteService = houseSiteService;
            await task.ImportDataAsync(fileName);
            Notifier.Success("数据传输完成");
        }

        #endregion Methods
    }
}