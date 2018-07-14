using System;

using Foundation;
using UIKit;
using XForm.Fields;
using XForm.Ios.FieldViews.Bases;

namespace XForm.Ios.FieldViews
{
    public partial class TextInputFieldView : ValueFieldView<TextInputField, string>
    {
        public static readonly UINib Nib;

        static TextInputFieldView()
        {
            Nib = UINib.FromName("TextInputFieldView", NSBundle.MainBundle);
        }

        protected TextInputFieldView(IntPtr handle) : base(handle)
        {
        }

        ~TextInputFieldView()
        {
            if (ValueTextField != null) 
                ValueTextField.EditingChanged -= ValueTextFieldEditingChanged;
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            ValueTextField.EditingChanged += ValueTextFieldEditingChanged;
        }

        private void ValueTextFieldEditingChanged(object sender, EventArgs e)
        {
            SetValue(ValueTextField.Text);
        }

        public override void TitleChanged(string value)
        {
            base.TitleChanged(value);

            TitleLabel.Text = value;
        }

        protected override void ValueChanged(string value)
        {
            base.ValueChanged(value);

            ValueTextField.Text = value;
        }
    }
}
