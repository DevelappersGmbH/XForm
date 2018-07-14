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

        public ButtonFieldView(ViewGroup parent) : base(parent, Resource.Layout.ButtonFieldView)
        {
            _button = ItemView.FindViewById<Button>(Resource.Id.button);
            _button.Click += HandleButtonClick;
        }

        ~ButtonFieldView()
        {
            if (_button != null) 
                _button.Click -= HandleButtonClick;
        }
        
        public override void BindTo(ButtonField field)
        {
            _button.Text = field.Title;
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
    }
}