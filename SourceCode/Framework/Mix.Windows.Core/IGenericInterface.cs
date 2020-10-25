using System;

namespace Mix.Windows.Core
{
    /// <summary>
    /// IGenericInterface
    /// </summary>
    public interface IGenericInterface
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        Type Type { get; }

        /// <summary>
        /// Gets the generic arguments.
        /// </summary>
        /// <value>
        /// The generic arguments.
        /// </value>
        Type[] GenericArguments { get; }

        /// <summary>
        /// Gets the method.
        /// </summary>
        /// <typeparam name="TDelegate">The type of the delegate.</typeparam>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="argTypes">The argument types.</param>
        /// <returns></returns>
        TDelegate GetMethod<TDelegate>(string methodName, params Type[] argTypes);
    }
}