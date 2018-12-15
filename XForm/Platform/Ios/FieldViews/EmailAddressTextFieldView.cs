using System;
using UIKit;

namespace XForm.Ios.FieldViews
{
    public class EmailAddressTextFieldView: SingleLineTextFieldView
    {
        public EmailAddressTextFieldView(IntPtr handle) : base(handle)
        {
        }

        internal override void Setup()
        {
            base.Setup();

            ValueTextField.KeyboardType = UIKeyboardType.EmailAddress;
        }
    }
}