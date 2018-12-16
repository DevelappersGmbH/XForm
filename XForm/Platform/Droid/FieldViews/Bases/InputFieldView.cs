using Android.Text;
using Android.Views;
using Android.Widget;
using XForm.Fields.Bases;

namespace XForm.Droid.FieldViews.Bases
{
    public abstract class InputFieldView<TField, TValue> : ValueFieldView<TField, TValue>
        where TField : InputField<TValue>
    {
        protected InputFieldView(ViewGroup parent, int layoutToInflate) : base(parent, layoutToInflate)
        {
            TitleTextView = ItemView.FindViewById<TextView>(Resource.Id.titleTextView);
            ValueEditText = ItemView.FindViewById<EditText>(Resource.Id.valueEditText);

            ValueEditText.TextChanged += ValueEditTextTextChanged;
        }

        protected InputFieldView(ViewGroup parent) : this(parent, Resource.Layout.XForm_EditTextFieldView)
        {
        }
        
        ~InputFieldView()
        {
            ValueEditText.TextChanged -= ValueEditTextTextChanged;
        }
        
        public TextView TitleTextView { get; }
        
        public EditText ValueEditText { get; }

        protected override void BindTo(TField field)
        {
            base.BindTo(field);
            
            ValueTextChanged(field?.ValueText ?? string.Empty);
        }

        protected override void TitleChanged(string value)
        {
            base.TitleChanged(value);

            TitleTextView.Text = value;
        }

        protected virtual void ValueTextChanged(string value)
        {
            if (Equals(ValueEditText.Text, value))
                return;
            
            ValueEditText.Text = value;
        }

        protected virtual void SetValueText(string value)
        {
            Field.ValueText = value;
        }

        protected override void EnabledChanged(bool value)
        {
            base.EnabledChanged(value);

            ValueEditText.Enabled = value;
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
        
        private void ValueEditTextTextChanged(object sender, TextChangedEventArgs e)
        {
            SetValueText(ValueEditText.Text);
        }
    }
}