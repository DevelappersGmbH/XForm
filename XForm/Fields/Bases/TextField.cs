namespace XForm.Fields.Bases
{
    public abstract class TextField: ValueField<string>
    {
        protected TextField(string title, string value = default(string)) : base(title, value)
        {
        }
    }
}