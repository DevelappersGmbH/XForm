using XForm.FieldAttributes;

namespace XForm.Fields
{
    public class PasswordTextFieldAttribute : DefaultFieldAttribute
    {
        public PasswordTextFieldAttribute(string name)
            : base(() => new PasswordTextField(name),
                   typeof(PasswordTextField).GetProperty(nameof(PasswordTextField.Value)))
        {
        }
    }
    
    public class PasswordTextField : SingleLineTextField
    {
        public PasswordTextField(string title, string value = default(string)) : base(title, value)
        {
        }
    }
}