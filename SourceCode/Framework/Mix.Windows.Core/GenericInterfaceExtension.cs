using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Mix.Windows.Core
{
    /// <summary>
    /// GenericInterfaceExtension
    /// </summary>
    public static class GenericInterfaceExtension
    {
        /// <summary>
        /// Ases the generic interface.
        /// </summary>
        /// <param name="this">The this.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static IGenericInterface AsGenericInterface(this object @this, Type type)
        {
            var interfaceType = (
                    from @interface in @this.GetType().GetInterfaces()
                    where @interface.IsGenericType
                    let definition = @interface.GetGenericTypeDefinition()
                    where definition == type
                    select @interface
                )
                .SingleOrDefault();

            return interfaceType != null
                ? new GenericInterfaceImpl(@this, interfaceType)
                : null;
        }

        /// <summary>
        /// GenericInterfaceImpl
        /// </summary>
        /// <seealso cref="Mix.Windows.Core.IGenericInterface" />
        private class GenericInterfaceImpl : IGenericInterface
        {
            private static readonly Regex ActionDelegateRegex = new Regex(@"^System\.Action(`\d{1,2})?", RegexOptions.Compiled);
            private static readonly Regex FuncDelegateRegex = new Regex(@"^System\.Func`(\d{1,2})", RegexOptions.Compiled);

            private readonly object _instance;

            /// <summary>
            /// Gets the type.
            /// </summary>
            /// <value>
            /// The type.
            /// </value>
            public Type Type { get; }

            /// <summary>
            /// Gets the generic arguments.
            /// </summary>
            /// <value>
            /// The generic arguments.
            /// </value>
            public Type[] GenericArguments => Type.GetGenericArguments();

            /// <summary>
            /// Initializes a new instance of the <see cref="GenericInterfaceImpl"/> class.
            /// </summary>
            /// <param name="instance">The instance.</param>
            /// <param name="interfaceType">Type of the interface.</param>
            public GenericInterfaceImpl(object instance, Type interfaceType)
            {
                _instance = instance;
                Type = interfaceType;
            }

            /// <summary>
            /// Gets the method.
            /// </summary>
            /// <typeparam name="TDelegate">The type of the delegate.</typeparam>
            /// <param name="methodName">Name of the method.</param>
            /// <param name="argTypes">The argument types.</param>
            /// <returns></returns>
            /// <exception cref="NotSupportedException"></exception>
            public TDelegate GetMethod<TDelegate>(string methodName, params Type[] argTypes)
            {
                switch (GetDelegateType<TDelegate>())
                {
                    case DelegateType.Action:
                        return GetAction<TDelegate>(methodName);

                    case DelegateType.ActionWithParams:
                        return GetActionWithParams<TDelegate>(methodName, argTypes);

                    default:
                        throw new NotSupportedException();
                }
            }

            /// <summary>
            /// Gets the action with parameters.
            /// </summary>
            /// <typeparam name="TDelegate">The type of the delegate.</typeparam>
            /// <param name="methodName">Name of the method.</param>
            /// <param name="argTypes">The argument types.</param>
            /// <returns></returns>
            /// <exception cref="ArgumentException">methodName</exception>
            private TDelegate GetActionWithParams<TDelegate>(string methodName, params Type[] argTypes)
            {
                var methodInfo = Type.GetMethod(methodName) ?? throw new ArgumentException(nameof(methodName));
                var argTypeList = argTypes.Any() ? argTypes : typeof(TDelegate).GetGenericArguments();
                (ParameterExpression expression, Type type)[] argObjectParameters = argTypeList
                    .Select(item => (Expression.Parameter(typeof(object)), item))
                    .ToArray();

                var method = Expression.Lambda<TDelegate>(
                        Expression.Call(
                            Expression.Constant(_instance),
                            methodInfo,
                            argObjectParameters.Select(item => Expression.Convert(item.expression, item.type))),
                        argObjectParameters.Select(item => item.expression))
                    .Compile();

                return method;
            }

            /// <summary>
            /// Gets the action.
            /// </summary>
            /// <typeparam name="TDelegate">The type of the delegate.</typeparam>
            /// <param name="methodName">Name of the method.</param>
            /// <returns></returns>
            /// <exception cref="ArgumentException">methodName</exception>
            private TDelegate GetAction<TDelegate>(string methodName)
            {
                var methodInfo = Type.GetMethod(methodName) ?? throw new ArgumentException(nameof(methodName));
                var method = Expression.Lambda<TDelegate>(
                        Expression.Call(
                            Expression.Constant(_instance),
                            methodInfo))
                    .Compile();

                return method;
            }

            /// <summary>
            /// Gets the type of the delegate.
            /// </summary>
            /// <typeparam name="TDelegate">The type of the delegate.</typeparam>
            /// <returns></returns>
            /// <exception cref="InvalidOperationException">
            /// </exception>
            private static DelegateType GetDelegateType<TDelegate>()
            {
                var actionMatch = ActionDelegateRegex.Match(typeof(TDelegate).FullName ?? throw new InvalidOperationException());
                if (actionMatch.Success)
                {
                    return actionMatch.Groups.Count > 1 ? DelegateType.ActionWithParams : DelegateType.Action;
                }

                var funcMatch = FuncDelegateRegex.Match(typeof(TDelegate).FullName ?? throw new InvalidOperationException());
                if (funcMatch.Success)
                {
                    return int.Parse(actionMatch.Groups[1].Value) > 1 ? DelegateType.FuncWithParams : DelegateType.Func;
                }

                return DelegateType.NotSupported;
            }

            /// <summary>
            ///
            /// </summary>
            private enum DelegateType
            {
                /// <summary>
                /// The not supported
                /// </summary>
                NotSupported,

                /// <summary>
                /// The action
                /// </summary>
                Action,

                /// <summary>
                /// The function
                /// </summary>
                Func,

                /// <summary>
                /// The action with parameters
                /// </summary>
                ActionWithParams,

                /// <summary>
                /// The function with parameters
                /// </summary>
                FuncWithParams
            }
        }
    }
}