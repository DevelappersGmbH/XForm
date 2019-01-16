using System;
using System.Globalization;
using XForm.InputConverters.Bases;

namespace XForm.InputConverters
{
    public class DoubleInputConverter : InputConverter<double?>
    {
        private readonly IFormatProvider _formatProvider;

        public DoubleInputConverter(IFormatProvider formatProvider = null) 
        {
            _formatProvider = formatProvider ?? CultureInfo.CurrentCulture;
        }

        public override double? ConvertText(string valueText)
        {
            return double.TryParse(valueText, NumberStyles.Any, _formatProvider, out var value) ? (double?) value : null;
        }

        public override string ConvertValue(double? value)
        {
            return value?.ToString(_formatProvider) ?? string.Empty;
        }
    }
}