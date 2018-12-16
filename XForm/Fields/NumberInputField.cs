using XForm.FieldAttributes;
using XForm.Fields.Bases;
using XForm.InputConverters;

namespace XForm.Fields
{
    public class NumberInputFieldAttribute : DefaultFieldAttribute
    {
        public NumberInputFieldAttribute(string name)
            : base(() => new NumberInputField(name, default(int)),
                   typeof(NumberInputField).GetProperty(nameof(NumberInputField.Value)))
        {
        }
    }
    
    public class NumberInputField : InputField<int?>
    {
        public NumberInputField(string title, int? value = null) : base(new IntInputConverter(), title, value)
        {
        }
    }
}