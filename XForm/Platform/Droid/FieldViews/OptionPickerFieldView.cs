using System;
using System.Collections;
using Android.Views;
using Android.Widget;
using XForm.Droid.FieldViews.Bases;
using XForm.Fields;
using XForm.Fields.Interfaces;

namespace XForm.Droid.FieldViews
{
    public class OptionPickerFieldView : ValueFieldView<IOptionPickerField, int?>
    {
        public OptionPickerFieldView(ViewGroup parent) : this(parent, Resource.Layout.XForm_SpinnerFieldView)
        {
        }

        public OptionPickerFieldView(ViewGroup parent, int layoutToInflate) : base(parent, layoutToInflate)
        {
            TitleTextView = ItemView.FindViewById<TextView>(Resource.Id.titleTextView);
            ValueSpinner = ItemView.FindViewById<Spinner>(Resource.Id.valueSpinner);
        }

        public TextView TitleTextView { get; }

        public Spinner ValueSpinner { get; }

        protected override void BindTo(IOptionPickerField field)
        {
            base.BindTo(field);

            var adapter = new ArrayAdapter(ItemView.Context,
                                           Android.Resource.Layout.SimpleSpinnerItem,
                                           (IList) field.OptionTexts);

            ValueSpinner.Adapter = adapter;
            ValueSpinner.OnItemSelectedListener = new SpinnerItemSelectedListener(field);
        }

        protected override void EnabledChanged(bool value)
        {
            base.EnabledChanged(value);

            ValueSpinner.Enabled = value;
        }

        protected override void TitleChanged(string value)
        {
            base.TitleChanged(value);

            TitleTextView.Text = Field.Title;
        }

        protected override void ValueChanged(int? value)
        {
            base.ValueChanged(value);

            if (!value.HasValue)
                return;

            ValueSpinner.SetSelection(value.Value, true);
        }

        private class SpinnerItemSelectedListener : Java.Lang.Object, AdapterView.IOnItemSelectedListener
        {
            private readonly WeakReference<IOptionPickerField> _fieldReference;
            
            public SpinnerItemSelectedListener(IOptionPickerField field)
            {
                _fieldReference = new WeakReference<IOptionPickerField>(field);
            }

            public void OnItemSelected(AdapterView parent, View view, int position, long id)
            {
                if (_fieldReference.TryGetTarget(out var field))
                    field.Value = position;
            }

            public void OnNothingSelected(AdapterView parent)
            {
            }
        }
    }
}