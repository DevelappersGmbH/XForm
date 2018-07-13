using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using XForm.Fields;

namespace XForm.Android.FieldViews
{
    public class LabelFieldView: RecyclerView.ViewHolder
    {
        private readonly TextView _titleTextView;
        private readonly TextView _valueTextView;

        public static LabelFieldView Create(ViewGroup parent)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.LabelFieldView, parent, false);
            return new LabelFieldView(view);
        }

        private LabelFieldView(View itemView) : base(itemView)
        {
            _titleTextView = itemView.FindViewById<TextView>(Resource.Id.titleTextView);
            _valueTextView = itemView.FindViewById<TextView>(Resource.Id.valueTextView);
        }

        public void BindTo(LabelField item)
        {
            _titleTextView.Text = item.Title;
            _valueTextView.Text = item.Value;
        }
    }
}