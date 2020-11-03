using Mix.Windows.WPF;
using Prism.Ioc;

namespace Mix.Desktop.Modules.Enterprise.ViewModels
{
    public class EnterpriseComponentViewModel : ViewModelBase
    {
        #region Ctor

        public EnterpriseComponentViewModel(IContainerExtension container) : base(container)
        {
        }

        #endregion Ctor
    }
}