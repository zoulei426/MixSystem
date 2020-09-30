using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Windows.Core
{
    public interface ILocalizable
    {
        I18nManager I18nManager { get; set; }

        void OnCurrentUICultureChanged();
    }
}
