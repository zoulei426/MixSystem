using Mix.Windows.WPF;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Desktop
{
    public class SignInPanelViewModel : ViewModelBase, IViewLoadedAndUnloadedAware<SignInPanel>
    {
        public SignInPanelViewModel(IContainerExtension container) : base(container)
        {
        }

        public void OnLoaded(SignInPanel view)
        {
        }

      

        public void OnUnloaded(SignInPanel view)
        {
        }

       
    }
}
