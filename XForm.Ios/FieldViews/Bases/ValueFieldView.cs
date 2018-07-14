using System;
using XForm.Fields.Interfaces;

namespace XForm.Ios.FieldViews.Bases
{
    public abstract class ValueFieldView<TField, TValue> : FieldView<TField> where TField : IField, IValueField<TValue>
    {
        protected ValueFieldView(IntPtr handle) : base(handle)
        {
        }
        
        protected override void BindTo(TField field)
        {
            base.BindTo(field);
            
            ValueChanged(field != null ? field.Value : default(TValue));
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

        protected virtual void ValueChanged(TValue value)
        {
        }
    }
}