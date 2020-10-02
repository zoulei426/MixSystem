using System;

namespace Mix.Windows.Core
{
    public interface IGenericInterface
    {
        Type Type { get; }

        Type[] GenericArguments { get; }

        TDelegate GetMethod<TDelegate>(string methodName, params Type[] argTypes);
    }
}