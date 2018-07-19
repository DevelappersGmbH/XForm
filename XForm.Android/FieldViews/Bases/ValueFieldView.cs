using Android.Views;
using XForm.Fields.Interfaces;

namespace XForm.Android.FieldViews.Bases
{
    public abstract class ValueFieldView<TField, TValue> : FieldView<TField> where TField : class, IField, IValueField<TValue>
    {
        protected ValueFieldView(ViewGroup parent, int layoutToInflate) : base(parent, layoutToInflate)
        {
        }

        protected override void BindTo(TField field)
        {
            base.BindTo(field);
            
            ValueChanged(field != null ? field.Value : default(TValue));
        }

        protected virtual void ValueChanged(TValue value)
        {
        }
        
        protected void SetValue(TValue value)
        {
            if (Field == null)
                return;

            Field.Value = value;
        }

        protected override void FieldPropertyChanged(string propertyName)
        {
            base.FieldPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(Field.Value):
                    ValueChanged(Field == null ? default(TValue) : Field.Value);
                    break;
            }
        }
    }
}