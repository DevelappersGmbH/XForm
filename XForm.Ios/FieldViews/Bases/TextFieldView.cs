using System;
using UIKit;
using XForm.Fields.Bases;
using XForm.Ios.ContentViews;
using XForm.Ios.ContentViews.Interfaces;

namespace XForm.Ios.FieldViews.Bases
{
    public abstract class TextFieldView<TField> : ValueFieldView<TField, ITextFieldContent, string> 
        where TField : TextField
    {
        private static ITextFieldContent CreateDefaultContentView()
        {
            return new TextFieldContent();
        }
        
        protected TextFieldView(IntPtr handle) : this(handle, CreateDefaultContentView) 
        {
        }

        protected TextFieldView(IntPtr handle, Func<ITextFieldContent> contentViewCreator) : base(handle, contentViewCreator)
        {
            ValueTextField.EditingChanged += ValueTextFieldEditingChanged;
        }

        ~TextFieldView()
        {
            if (ValueTextField != null)
                ValueTextField.EditingChanged -= ValueTextFieldEditingChanged;
        }
        
        public UILabel TitleLabel => Content.TitleLabel;
        
        public UITextField ValueTextField => Content.ValueTextField;
        
        protected override void TitleChanged(string value)
        {
            base.TitleChanged(value);

            TitleLabel.Text = value;
        }

        protected override void ValueChanged(string value)
        {
            base.ValueChanged(value);

            ValueTextField.Text = value;
        }

        private void ValueTextFieldEditingChanged(object sender, EventArgs e)
        {
            SetValue(ValueTextField.Text);
        }
    }
}