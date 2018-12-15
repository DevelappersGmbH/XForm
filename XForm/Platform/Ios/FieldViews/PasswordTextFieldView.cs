using System;

namespace XForm.Ios.FieldViews
{
    public class PasswordTextFieldView: SingleLineTextFieldView
    {
        public PasswordTextFieldView(IntPtr handle) : base(handle)
        {
        }

        internal override void Setup()
        {
            base.Setup();

            ValueTextField.SecureTextEntry = true;
        }
    }
}