using System.Windows.Input;
using XForm.Fields.Bases;

namespace XForm.Fields
{
    public class ButtonField : ValueField<ICommand>
    {
        public ButtonField(string title, ICommand value) : base(title, value)
        {
        }
    }
}