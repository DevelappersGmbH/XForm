using Android.Text;
using Android.Views;
using XForm.Droid.FieldViews.Bases;
using XForm.Fields;

namespace XForm.Droid.FieldViews
{
    public class NumberInputFieldView : InputFieldView<NumberInputField, int?>
    {
        public NumberInputFieldView(ViewGroup parent, int layoutToInflate) : base(parent, layoutToInflate)
        {
            Initialize();
        }

        public NumberInputFieldView(ViewGroup parent) : base(parent)
        {
            Initialize();
        }
        
        private void Initialize()
        {
            ValueEditText.InputType = InputTypes.ClassNumber | InputTypes.NumberFlagSigned;
        }
    }
}