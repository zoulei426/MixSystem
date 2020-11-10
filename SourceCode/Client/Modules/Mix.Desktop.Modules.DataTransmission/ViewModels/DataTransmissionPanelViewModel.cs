using CsvHelper;
using Mix.Core;
using Mix.Desktop.Modules.DataTransmission.Business;
using Mix.Library.Services;
using Mix.Windows.WPF;
using Prism.Commands;
using Prism.Ioc;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
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
        public ICommand ExportCSVCommand { get; set; }

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
            ExportCSVCommand = new DelegateCommand(ExportCSV);
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

        private async void ExportCSV()
        {
            await Task.Run(async () =>
            {
                var jcxxes = await houseSiteService.GetJcxxesAsync();
                await WriteSCVAsync(@"D:\Users\zoulei\Desktop\输出\基础信息.csv", jcxxes);
                Notifier.Success("基础信息导出完成");

                var cyxxes = await houseSiteService.GetCyxxesAsync();
                await WriteSCVAsync(@"D:\Users\zoulei\Desktop\输出\成员信息.csv", cyxxes);
                Notifier.Success("成员信息导出完成");

                var nfxxes = await houseSiteService.GetNfxxesAsync();
                await WriteSCVAsync(@"D:\Users\zoulei\Desktop\输出\农房信息.csv", nfxxes);
                Notifier.Success("农房信息导出完成");
            });
        }

        private async Task WriteSCVAsync<T>(string outPath, IEnumerable<T> records)
        {
            await Task.Run(() =>
            {
                using FileStream fileStream = new FileStream(outPath, FileMode.Create, FileAccess.ReadWrite);
                using var writer = new StreamWriter(fileStream, Encoding.UTF8);
                using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
                csv.WriteRecords(records);
            });
        }

        #endregion Methods
    }
}