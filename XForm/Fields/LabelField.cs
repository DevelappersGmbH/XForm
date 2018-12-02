using XForm.Fields.Bases;
using XForm.Forms;

namespace XForm.Fields
{
    public class LabelFieldAttribute : DefaultFieldAttribute
    {
        public LabelFieldAttribute(string name)
            : base(() => new LabelField(name, default(string)),
                   typeof(LabelField).GetProperty(nameof(LabelField.Value)))
        {
        }
    }

    public class LabelField : ValueField<string>
    {
        public LabelField(string title, string value) : base(title, value)
        {
        }
    }
}