using XForm.InputConverters.Bases;

namespace XForm.InputConverters
{
    public class DoubleInputConverter : InputConverter<double?>
    {
        public override double? ConvertText(string valueText)
        {
            return double.TryParse(valueText, out var value) ? (double?) value : null;
        }

        public override string ConvertValue(double? value)
        {
            return value?.ToString() ?? string.Empty;
        }
    }
}