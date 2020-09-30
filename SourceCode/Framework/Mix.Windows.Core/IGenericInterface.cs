using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Windows.Core
{
    public interface IGenericInterface
    {
        Type Type { get; }

        Type[] GenericArguments { get; }

        TDelegate GetMethod<TDelegate>(string methodName, params Type[] argTypes);
    }
}
