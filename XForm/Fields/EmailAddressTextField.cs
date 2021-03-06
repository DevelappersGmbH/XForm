using XForm.FieldAttributes;

namespace XForm.Fields
{
    public class EmailAddressTextFieldAttribute : DefaultFieldAttribute
    {
        public EmailAddressTextFieldAttribute(string name)
            : base(() => new EmailAddressTextField(name),
                   typeof(EmailAddressTextField).GetProperty(nameof(EmailAddressTextField.Value)))
        {
        }
    }
    
    public class EmailAddressTextField : SingleLineTextField
    {
        public EmailAddressTextField(string title, string value = default(string)) : base(title, value)
        {
        }
    }
}