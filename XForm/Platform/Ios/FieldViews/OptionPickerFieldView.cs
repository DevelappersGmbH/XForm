using System;
using CoreGraphics;
using UIKit;
using XForm.EventSubscription;
using XForm.Fields.Interfaces;
using XForm.Ios.ContentViews;
using XForm.Ios.ContentViews.Interfaces;
using XForm.Ios.FieldViews.Bases;

namespace XForm.Ios.FieldViews
{
    public class OptionPickerFieldView : ValueFieldView<IOptionPickerField, ITitleButtonFieldContent, int?>
    {
        private IDisposable _buttonTouchUpInsideSubscription;
        
        public OptionPickerFieldView(IntPtr handle) : base(handle)
        {
        }

        internal override Func<ITitleButtonFieldContent> DefaultContentCreator { get; } = () => new TitleButtonFieldContent();
        
        public UILabel TitleLabel => Content.TitleLabel;

        public UIButton Button => Content.Button;

        internal override void Setup()
        {
            base.Setup();
            
            _buttonTouchUpInsideSubscription = Button.GetType()
                                                     .GetEvent(nameof(Button.TouchUpInside))
                                                     .WeakSubscribe(Button, ButtonTouchUpInside);
        }

        protected override void EnabledChanged(bool value)
        {
            base.EnabledChanged(value);

            Button.Enabled = value;
            ResignFirstResponder();
        }

        protected override void TitleChanged(string value)
        {
            base.TitleChanged(value);

            TitleLabel.Text = Field.Title;
        }

        protected override void ValueChanged(int? value)
        {
            base.ValueChanged(value);

            Button.SetTitle(Field.SelectedOptionText ?? "Option", UIControlState.Normal);
        }
        
        private void ButtonTouchUpInside(object sender, EventArgs e)
        {
            BecomeFirstResponder();

            // Set first option as default
            if (!Field.Value.HasValue && Field.OptionsCount > 0)
            {
                Field.Value = 0;
            }
        }
        
        private void DoneTouchUp(object sender, EventArgs e)
        {
            ResignFirstResponder();
        }

        public override bool CanBecomeFirstResponder => true;

        public override UIView InputAccessoryView
        {
            get
            {
                var toolbar = new UIToolbar(new CGRect(0, 0, 200, 44));

                toolbar.Items = new[]
                {
                    new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
                    new UIBarButtonItem(UIBarButtonSystemItem.Done, DoneTouchUp)
                };
                
                return toolbar;
            }
        }

        public override UIView InputView
        {
            get
            {
                var pickerView = new UIPickerView();
                
                pickerView.Model = new PickerViewModel(Field);
                
                if (Field.Value.HasValue)
                    pickerView.Select(Field.Value.Value, 0, true);
                
                return pickerView;
            }
        }
        
        private class PickerViewModel: UIPickerViewModel
        {
            private readonly WeakReference<IOptionPickerField> _fieldReference;
            
            public PickerViewModel(IOptionPickerField field)
            {
                _fieldReference = new WeakReference<IOptionPickerField>(field);
            }

            public override nint GetComponentCount(UIPickerView pickerView)
            {
                return 1;
            }

            public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
            {
                return _fieldReference.TryGetTarget(out var field) ? field.OptionsCount : 0;
            }

            public override string GetTitle(UIPickerView pickerView, nint row, nint component)
            {
                return _fieldReference.TryGetTarget(out var field) ? field.OptionTextForValue((int) row) : string.Empty;
            }

            public override void Selected(UIPickerView pickerView, nint row, nint component)
            {
                if (_fieldReference.TryGetTarget(out var field))
                    field.Value = (int) row;
            }
        }
    }
}