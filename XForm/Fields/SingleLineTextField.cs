using XForm.FieldAttributes;
using XForm.Fields.Bases;

namespace XForm.Fields
{
    public class SingleLineTextFieldAttribute : DefaultFieldAttribute
    {
        public SingleLineTextFieldAttribute(string name)
            : base(() => new SingleLineTextField(name),
                   typeof(SingleLineTextField).GetProperty(nameof(SingleLineTextField.Value)))
        {
        }
    }
    
    public class SingleLineTextField : TextField
    {
        public SingleLineTextField(string title, string value = default(string)) : base(title, value)
        {
        }
    }
}