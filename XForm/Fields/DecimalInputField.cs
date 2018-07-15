using XForm.Fields.Bases;
using XForm.InputConverters;

namespace XForm.Fields
{
    public class DecimalInputField : InputField<double?>
    {
        public DecimalInputField(string title, double? value = null) : base(new DoubleInputConverter(), title, value)
        {
        }
    }
}