using Android.Views;
using Android.Widget;
using XForm.Android.FieldViews.Bases;
using XForm.Fields;

namespace XForm.Android.FieldViews
{
    public class LabelFieldView: FieldView<LabelField>
    {
        private readonly TextView _titleTextView;
        private readonly TextView _valueTextView;

        public LabelFieldView(ViewGroup parent) : base(parent, Resource.Layout.LabelFieldView)
        {
            _titleTextView = ItemView.FindViewById<TextView>(Resource.Id.titleTextView);
            _valueTextView = ItemView.FindViewById<TextView>(Resource.Id.valueTextView);
        }

        public override void BindTo(LabelField item)
        {
            _titleTextView.Text = item.Title;
            _valueTextView.Text = item.Value;
        }
    }
}