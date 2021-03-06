using Android.Views;
using Android.Widget;
using XForm.Droid.FieldViews.Bases;
using XForm.Fields;

namespace XForm.Droid.FieldViews
{
    public class LabelFieldView: ValueFieldView<LabelField, string>
    {
        private readonly TextView _titleTextView;
        private readonly TextView _valueTextView;

        public LabelFieldView(ViewGroup parent) : base(parent, Resource.Layout.XForm_LabelFieldView)
        {
            _titleTextView = ItemView.FindViewById<TextView>(Resource.Id.titleTextView);
            _valueTextView = ItemView.FindViewById<TextView>(Resource.Id.valueTextView);
        }

        protected override void TitleChanged(string value)
        {
            base.TitleChanged(value);

            _titleTextView.Text = value;
        }

        protected override void ValueChanged(string value)
        {
            base.ValueChanged(value);

            _valueTextView.Text = value;
        }
    }
}