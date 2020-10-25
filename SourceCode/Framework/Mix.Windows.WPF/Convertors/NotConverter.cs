namespace Mix.Windows.WPF.Convertors
{
    /// <summary>
    /// 非转换器
    /// </summary>
    /// <seealso cref="Mix.Windows.WPF.Convertors.ValueConverterBase{T,T}" />
    public class NotConverter : ValueConverterBase<bool, bool>
    {
        /// <summary>
        /// Converts the specified value.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        protected override bool Convert(bool value) => !value;

        /// <summary>
        /// Converts the back.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        protected override bool ConvertBack(bool value) => Convert(value);
    }
}