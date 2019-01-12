using System;
using System.Globalization;
using XForm.InputConverters.Bases;

namespace XForm.InputConverters
{
    public class IntInputConverter : InputConverter<int?>
    {
        private readonly IFormatProvider _formatProvider;

        public IntInputConverter(IFormatProvider formatProvider = null) 
        {
            _formatProvider = formatProvider ?? CultureInfo.CurrentCulture;
        }

        public override int? ConvertText(string valueText)
        {
            return int.TryParse(valueText, NumberStyles.Any, _formatProvider, out var value) ? (int?) value : null;
        }

        public override string ConvertValue(int? value)
        {
            return value?.ToString(_formatProvider) ?? string.Empty;
        }
    }
}