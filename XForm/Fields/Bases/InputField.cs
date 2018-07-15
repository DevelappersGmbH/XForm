using System;
using XForm.Fields.Interfaces;
using XForm.InputConverters.Bases;

namespace XForm.Fields.Bases
{
    public abstract class InputField<TValue> : ValueField<TValue>, IInputField
    {
        private readonly InputConverter<TValue> _inputConverter;
        
        private string _valueText;

        protected InputField(InputConverter<TValue> inputConverter, string title, TValue value = default(TValue)) : base(title, value)
        {
            _inputConverter = inputConverter;
        }

        public event EventHandler ValueTextChanged;

        public string ValueText
        {
            get => _valueText;
            set
            {
                if (Set(ref _valueText, value, ValueTextChanged))
                {
                    Value = _inputConverter.ConvertText(value);
                }
            }
        }
    }
}