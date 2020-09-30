using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Windows.WPF
{
    public interface IViewLoadedAndUnloadedAware
    {
        void OnLoaded();

        void OnUnloaded();
    }

    public interface IViewLoadedAndUnloadedAware<in TView>
    {
        void OnLoaded(TView view);

        void OnUnloaded(TView view);
    }
}
