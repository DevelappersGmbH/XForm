using XForm.Fields.Bases;

namespace XForm.Fields
{
    public class TextInputField: ValueField<string>
    {
        public TextInputField(string title, string value = default(string)) : base(title, value)
        {
        }
    }
}