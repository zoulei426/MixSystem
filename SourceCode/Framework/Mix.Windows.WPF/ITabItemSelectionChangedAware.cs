using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Windows.WPF
{
    public interface ITabItemSelectionChangedAware
    {
        void OnSelected();

        void OnUnselected();
    }
}
