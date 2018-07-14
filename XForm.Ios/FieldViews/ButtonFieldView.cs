using System;
using Foundation;
using UIKit;
using XForm.Fields;
using XForm.Ios.FieldViews.Bases;

namespace XForm.Ios.FieldViews
{
    public partial class ButtonFieldView : FieldView<ButtonField>
    {
        public static readonly UINib Nib;

        static ButtonFieldView()
        {
            Nib = UINib.FromName("ButtonFieldView", NSBundle.MainBundle);
        }

        protected ButtonFieldView(IntPtr handle) : base(handle)
        {
        }

        ~ButtonFieldView()
        {
            if (Button != null)
                Button.TouchUpInside -= ButtonTouchUpInside;
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            Button.TouchUpInside += ButtonTouchUpInside;
        }

        public override void TitleChanged(string value)
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
