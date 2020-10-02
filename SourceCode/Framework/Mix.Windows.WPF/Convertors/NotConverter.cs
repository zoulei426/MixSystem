namespace Mix.Windows.WPF.Convertors
{
    public class NotConverter : ValueConverterBase<bool, bool>
    {
        protected override bool Convert(bool value) => !value;

        protected override bool ConvertBack(bool value) => Convert(value);
    }
}