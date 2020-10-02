using Mix.Windows.WPF;
using Prism.Ioc;

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