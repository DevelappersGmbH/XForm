using System;

namespace XForm.Fields.Interfaces
{
    public interface IValueField : IField {
        event EventHandler ValueChanged;
    }
    
    public interface IValueField<TValue> : IValueField
    {
        TValue Value { get; set; }
    }
}