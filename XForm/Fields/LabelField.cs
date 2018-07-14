using XForm.Fields.Bases;

namespace XForm.Fields
{
    public class LabelField: ValueField<string>
    {
        public LabelField(string title, string value) : base(title, value)
        {
        }
    }
}