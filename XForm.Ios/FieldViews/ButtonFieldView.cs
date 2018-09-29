using System;
using System.Windows.Input;
using UIKit;
using XForm.EventSubscription;
using XForm.Fields;
using XForm.Ios.ContentViews;
using XForm.Ios.ContentViews.Interfaces;
using XForm.Ios.FieldViews.Bases;

namespace XForm.Ios.FieldViews
{
    public class ButtonFieldView: ValueFieldView<ButtonField, IButtonFieldContent, ICommand>
    {
        private IDisposable _buttonTouchUpInsideSubscription;
        
        public ButtonFieldView(IntPtr handle) : this(handle, () => new ButtonFieldContent())
        {
        }
        
        public ButtonFieldView(IntPtr handle, Func<IButtonFieldContent> createContent) : base(handle, createContent)
        {
            _buttonTouchUpInsideSubscription = Button.GetType().GetEvent(nameof(Button.TouchUpInside)).WeakSubscribe(Button, ButtonTouchUpInside);
        }

        public UIButton Button => Content.Button;

        protected override void TitleChanged(string value)
        {
            base.TitleChanged(value);
            
            Button.SetTitle(value, UIControlState.Normal);
        }

        protected override void EnabledChanged(bool value)
        {
            base.EnabledChanged(value);

            Button.Enabled = value;
        }

        private void ButtonTouchUpInside(object sender, EventArgs e)
        {
            var command = Field.Value;

            if (command == null)
                return;
            
            if (!command.CanExecute(null))
                return;
            
            command.Execute(null);
        }
    }
}