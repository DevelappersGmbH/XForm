namespace XForm.InputConverters.Bases
{
    public abstract class InputConverter<T>
    {
        public abstract T ConvertText(string valueText);
        public abstract string ConvertValue(T value);
    }
}