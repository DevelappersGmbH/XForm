using Android.Text;
using Android.Views;
using Android.Widget;
using XForm.Android.FieldViews.Bases;
using XForm.Fields.Bases;

namespace XForm.Android.FieldViews
{
    public class TextInputFieldView : ValueFieldView<TextField, string>
    {
        private readonly TextView _titleTextView;
        private readonly EditText _valueEditText;
        
        public TextInputFieldView(ViewGroup parent) : base(parent, Resource.Layout.TextInputFieldView)
        {
            _titleTextView = ItemView.FindViewById<TextView>(Resource.Id.titleTextView);
            _valueEditText = ItemView.FindViewById<EditText>(Resource.Id.valueEditText);

            _valueEditText.TextChanged += ValueEditTextTextChanged;
        }

        ~TextInputFieldView()
        {
            if (_valueEditText != null) 
                _valueEditText.TextChanged -= ValueEditTextTextChanged;
        }

        protected override void TitleChanged(string value)
        {
            base.TitleChanged(value);

            _titleTextView.Text = value;
        }

        protected override void ValueChanged(string value)
        {
            base.ValueChanged(value);

            if (Equals(_valueEditText.Text, value))
                return;
            
            _valueEditText.Text = value;
        }
        
        private void ValueEditTextTextChanged(object sender, TextChangedEventArgs e)
        {
            SetValue(_valueEditText.Text);
        }
    }
}
