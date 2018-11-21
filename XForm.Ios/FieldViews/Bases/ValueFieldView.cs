using System;
using XForm.Fields.Interfaces;
using XForm.Ios.ContentViews.Interfaces;

namespace XForm.Ios.FieldViews.Bases
{
    public abstract class ValueFieldView<TField, TContent, TValue> : FieldView<TField, TContent> 
        where TField : class, IValueField<TValue>
        where TContent : class, IFieldContent
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

        protected virtual void SetValue(TValue value)
        {
            if (Field == null)
                return;

            Field.Value = value;
        }
    }
}