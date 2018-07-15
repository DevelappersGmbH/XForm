using System.Globalization;
using XForm.InputConverters.Bases;

namespace XForm.InputConverters
{
    public class IntInputConverter : InputConverter<int?>
    {
        public override int? ConvertText(string valueText)
        {
            return int.TryParse(valueText, NumberStyles.Any, null, out var value) ? (int?) value : null;
        }

        public override string ConvertValue(int? value)
        {
            return value?.ToString() ?? string.Empty;
        }
    }
}