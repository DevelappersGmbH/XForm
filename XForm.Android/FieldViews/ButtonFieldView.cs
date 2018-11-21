using System;
using Android.Views;
using Android.Widget;
using XForm.Android.FieldViews.Bases;
using XForm.Fields;

namespace XForm.Android.FieldViews
{
    public class ButtonFieldView: FieldView<ButtonField>
    {
        private readonly Button _button;

        public ButtonFieldView(ViewGroup parent) : base(parent, Resource.Layout.XForm_ButtonFieldView)
        {
            _button = ItemView.FindViewById<Button>(Resource.Id.button);
            _button.Click += HandleButtonClick;
        }

        ~ButtonFieldView()
        {
            if (_button != null) 
                _button.Click -= HandleButtonClick;
        }

        protected override void TitleChanged(string value)
        {
            base.TitleChanged(value);

            _button.Text = value;
        }

        private void HandleButtonClick(object sender, EventArgs e)
        {
            var command = Field.Value;
            
            if (command == null)
                return;
            
            if (!command.CanExecute(null))
                return;
            
            command.Execute(null);
        }

        protected override void EnabledChanged(bool value)
        {
            base.EnabledChanged(value);

            _button.Enabled = value;
        }
    }
}