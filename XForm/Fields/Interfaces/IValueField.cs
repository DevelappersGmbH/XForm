namespace XForm.Fields.Interfaces
{
    public interface IValueField<TValue> : IField
    {
        TValue Value { get; set; }
    }
}