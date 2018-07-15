using XForm.Fields.Bases;
using XForm.InputConverters;

namespace XForm.Fields
{
    public class NumberInputField : InputField<int?>
    {
        public NumberInputField(string title, int? value = null) : base(new IntInputConverter(), title, value)
        {
        }
    }
}