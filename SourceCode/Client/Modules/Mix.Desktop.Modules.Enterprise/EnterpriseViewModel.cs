using Mix.Windows.WPF;
using Prism.Ioc;

namespace Mix.Desktop.Modules.Enterprise
{
    public class EnterpriseViewModel : ViewModelBase
    {
        protected IEnterpriseApi EnterpriseApi { get; }

        public EnterpriseViewModel(IContainerExtension container) : base(container)
        {
            EnterpriseApi = Container.Resolve<IEnterpriseApi>();
        }
    }
}