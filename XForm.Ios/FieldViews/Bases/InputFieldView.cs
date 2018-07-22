using System;
using UIKit;
using XForm.Fields.Bases;
using XForm.Ios.ContentViews;
using XForm.Ios.ContentViews.Interfaces;

namespace XForm.Ios.FieldViews.Bases
{
    public abstract class InputFieldView<TField, TValue> : ValueFieldView<TField, ITextFieldContent, TValue> 
        where TField : InputField<TValue>
    {
        protected InputFieldView(IntPtr handle) : this(handle, () => new TextFieldContent()) 
        {
        }

        protected InputFieldView(IntPtr handle, Func<ITextFieldContent> contentViewCreator) : base(handle, contentViewCreator)
        {
            ValueTextField.EditingChanged += ValueTextFieldEditingChanged;
        }

        ~InputFieldView()
        {
            if (ValueTextField != null)
                ValueTextField.EditingChanged -= ValueTextFieldEditingChanged;
        }

        public UILabel TitleLabel => Content.TitleLabel;
        
        public UITextField ValueTextField => Content.ValueTextField;

        protected override void BindTo(TField field)
        {
            base.BindTo(field);
            
            ValueTextChanged(field.ValueText);
        }

        protected override void TitleChanged(string value)
        {
            base.TitleChanged(value);

            TitleLabel.Text = value;
        }

        protected virtual void ValueTextChanged(string value)
        {
            ValueTextField.Text = value;
        }

        protected virtual void SetValueText(string value)
        {
            Field.ValueText = value;
        }

        protected override void EnabledChanged(bool value)
        {
            base.EnabledChanged(value);

            ValueTextField.Enabled = value;
        }

        private void ValueTextFieldEditingChanged(object sender, EventArgs e)
        {
            SetValueText(ValueTextField.Text);
        }

        protected override void FieldPropertyChanged(string propertyName)
        {
            base.FieldPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(Field.ValueText):
                    ValueTextChanged(Field.ValueText);
                    break;
            }
        }
    }
}