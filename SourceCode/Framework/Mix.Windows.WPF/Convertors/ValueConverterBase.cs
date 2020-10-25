using Mix.Core;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Mix.Windows.WPF.Convertors
{
    /// <summary>
    /// 转换器基类
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TTarget">The type of the target.</typeparam>
    /// <typeparam name="TParameter">The type of the parameter.</typeparam>
    /// <seealso cref="System.Windows.Data.IValueConverter" />
    public abstract class ValueConverterBase<TSource, TTarget, TParameter> : IValueConverter
    {
        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns <see langword="null" />, the valid null value is used.
        /// </returns>
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value.CastTo<TSource>(), parameter.CastTo<TParameter>());
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns <see langword="null" />, the valid null value is used.
        /// </returns>
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertBack(value.CastTo<TTarget>(), parameter.CastTo<TParameter>());
        }

        /// <summary>
        /// Converts the non null value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        protected virtual TTarget ConvertNonNullValue(TSource value, TParameter parameter) => throw new NotSupportedException();

        /// <summary>
        /// Converts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        protected virtual TTarget Convert(TSource value, TParameter parameter)
        {
            return value != null ? ConvertNonNullValue(value, parameter) : default;
        }

        /// <summary>
        /// Converts the back.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        protected virtual TSource ConvertBack(TTarget value, TParameter parameter) => throw new NotSupportedException();
    }

    /// <summary>
    /// ValueConverterBase
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TTarget">The type of the target.</typeparam>
    /// <seealso cref="System.Windows.Data.IValueConverter" />
    public abstract class ValueConverterBase<TSource, TTarget> : ValueConverterBase<TSource, TTarget, object>
    {
        /// <summary>
        /// Converts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        protected sealed override TTarget Convert(TSource value, object parameter) => Convert(value);

        /// <summary>
        /// Converts the non null value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        protected sealed override TTarget ConvertNonNullValue(TSource value, object parameter) => throw new NotSupportedException();

        /// <summary>
        /// Converts the back.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        protected sealed override TSource ConvertBack(TTarget value, object parameter) => ConvertBack(value);

        /// <summary>
        /// Converts the non null value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        protected virtual TTarget ConvertNonNullValue(TSource value) => throw new NotSupportedException();

        /// <summary>
        /// Converts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        protected virtual TTarget Convert(TSource value) => value != null ? ConvertNonNullValue(value) : default;

        /// <summary>
        /// Converts the back.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        protected virtual TSource ConvertBack(TTarget value) => throw new NotSupportedException();
    }
}