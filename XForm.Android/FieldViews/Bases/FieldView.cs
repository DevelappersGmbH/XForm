using Android.Support.V7.Widget;
using Android.Views;
using XForm.Fields.Interfaces;
using XForm.FieldViews;

namespace XForm.Android.FieldViews.Bases
{
    public abstract class FieldView : RecyclerView.ViewHolder, IFieldView
    {
        private static View CreateViewItem(ViewGroup parent, int layout)
        {
            return LayoutInflater
                   .From(parent.Context)
                   .Inflate(layout, parent, false);
        }

        protected FieldView(ViewGroup parent, int layoutToInflate) : base(CreateViewItem(parent, layoutToInflate))
        {
        }

        public abstract void BindTo(IField field);
    }

    public abstract class FieldView<TField> : FieldView
    {
        protected FieldView(ViewGroup parent, int layoutToInflate) : base(parent, layoutToInflate)
        {
        }

        public override void BindTo(IField field)
        {
            BindTo((TField) field);
        }

        public abstract void BindTo(TField field);
    }
}