using Android.Text;
using Android.Views;
using Android.Widget;
using XForm.Fields.Bases;

namespace XForm.Android.FieldViews.Bases
{
    public abstract class TextFieldView<TField> : ValueFieldView<TField, string>
        where TField : TextField
    {
        protected TextFieldView(ViewGroup parent) : this(parent, Resource.Layout.XForm_EditTextFieldView)
        {
        }
        
        protected TextFieldView(ViewGroup parent, int layoutToInflate) : base(parent, layoutToInflate)
        {
            TitleTextView = ItemView.FindViewById<TextView>(Resource.Id.titleTextView);
            ValueEditText = ItemView.FindViewById<EditText>(Resource.Id.valueEditText);

            ValueEditText.TextChanged += ValueEditTextTextChanged;
        }

        ~TextFieldView()
        {
            ValueEditText.TextChanged -= ValueEditTextTextChanged;
        }
        
        public TextView TitleTextView { get; }
        
        public EditText ValueEditText { get; }

        protected override void TitleChanged(string value)
        {
            base.TitleChanged(value);

            TitleTextView.Text = value;
        }

        protected override void ValueChanged(string value)
        {
            if (Equals(ValueEditText.Text, value))
                return;
            
            ValueEditText.Text = value;
        }
        
        private void ValueEditTextTextChanged(object sender, TextChangedEventArgs e)
        {
            SetValue(ValueEditText.Text);
        }
        
        protected override void EnabledChanged(bool value)
        {
            base.EnabledChanged(value);

            ValueEditText.Enabled = value;
        }
    }
}