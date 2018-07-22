using System;
using XForm.Fields.Interfaces;

namespace XForm.Fields.Bases
{
    public abstract class ValueField<TValue> : Field, IValueField<TValue>
    {
        private TValue _value;

        protected ValueField(string title, TValue value = default(TValue)) : base(title)
        {
            Value = value;
        }

        public event EventHandler ValueChanged;

        public TValue Value
        {
            get => _value;
            set
            {
                var oldValue = _value;
                
                if (!Set(ref _value, value, ValueChanged))
                    return;
                
                HandleValueChanged(oldValue, value);
            }
        }

        protected virtual void HandleValueChanged(TValue oldValue, TValue newValue)
        {
        }
    }
}