using System;
using System.Windows.Input;
using UIKit;
using XForm.Fields;
using XForm.Ios.ContentViews;
using XForm.Ios.ContentViews.Interfaces;
using XForm.Ios.FieldViews.Bases;

namespace XForm.Ios.FieldViews
{
    public class ButtonFieldView: ValueFieldView<ButtonField, IButtonFieldContent, ICommand>
    {
        public ButtonFieldView(IntPtr handle) : this(handle, () => new ButtonFieldContent())
        {
        }
        
        public ButtonFieldView(IntPtr handle, Func<IButtonFieldContent> createContent) : base(handle, createContent)
        {
            Button.TouchUpInside += ButtonTouchUpInside;
        }
        
        ~ButtonFieldView()
        {
            if (Button != null)
                Button.TouchUpInside -= ButtonTouchUpInside;
        }

        public UIButton Button => Content.Button;

        protected override void TitleChanged(string value)
        {
            base.TitleChanged(value);
            
            Button.SetTitle(value, UIControlState.Normal);
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