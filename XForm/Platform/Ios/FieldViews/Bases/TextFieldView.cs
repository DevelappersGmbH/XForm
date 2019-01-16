using System;
using UIKit;
using XForm.EventSubscription;
using XForm.Fields.Bases;
using XForm.Ios.ContentViews;
using XForm.Ios.ContentViews.Interfaces;

namespace XForm.Ios.FieldViews.Bases
{
    public abstract class TextFieldView<TField> : ValueFieldView<TField, ITextFieldContent, string> 
        where TField : TextField
    {
        private IDisposable _valueTextFieldEditingChangedSubscription;
        
        protected TextFieldView(IntPtr handle) : base(handle) 
        {
        }

        internal override Func<ITextFieldContent> DefaultContentCreator { get; } = () => new TextFieldContent();
        
        public UILabel TitleLabel => Content.TitleLabel;
        
        public UITextField ValueTextField => Content.ValueTextField;

        internal override void Setup()
        {
            base.Setup();
            
            _valueTextFieldEditingChangedSubscription = ValueTextField.GetType()
                                                                      .GetEvent(nameof(ValueTextField.EditingChanged))
                                                                      .WeakSubscribe(ValueTextField, ValueTextFieldEditingChanged);
        }

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

        protected override void EnabledChanged(bool value)
        {
            base.EnabledChanged(value);

            ValueTextField.Enabled = value;
        }

        private void ValueTextFieldEditingChanged(object sender, EventArgs e)
        {
            SetValue(ValueTextField.Text);
        }
    }
}