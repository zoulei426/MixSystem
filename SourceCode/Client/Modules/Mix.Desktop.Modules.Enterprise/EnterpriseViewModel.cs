using Mix.Windows.WPF;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
