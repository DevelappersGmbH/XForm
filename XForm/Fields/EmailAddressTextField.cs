using XForm.Forms;

namespace XForm.Fields
{
    public class EmailAddressTextFieldAttribute : FieldAttribute
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