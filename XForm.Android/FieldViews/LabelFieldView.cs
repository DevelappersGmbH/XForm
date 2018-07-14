using Android.Views;
using Android.Widget;
using XForm.Android.FieldViews.Bases;
using XForm.Fields;

namespace XForm.Android.FieldViews
{
    public class LabelFieldView: ValueFieldView<LabelField, string>
    {
        private readonly TextView _titleTextView;
        private readonly TextView _valueTextView;

        public LabelFieldView(ViewGroup parent) : base(parent, Resource.Layout.LabelFieldView)
        {
            _titleTextView = ItemView.FindViewById<TextView>(Resource.Id.titleTextView);
            _valueTextView = ItemView.FindViewById<TextView>(Resource.Id.valueTextView);
        }

        public override void TitleChanged(string value)
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