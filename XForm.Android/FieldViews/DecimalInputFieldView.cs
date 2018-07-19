using Android.Text;
using Android.Views;
using XForm.Android.FieldViews.Bases;
using XForm.Fields;

namespace XForm.Android.FieldViews
{
    public class DecimalInputFieldView : InputFieldView<DecimalInputField, double?>
    {
        public DecimalInputFieldView(ViewGroup parent, int layoutToInflate) : base(parent, layoutToInflate)
        {
            Initialize();
        }

        public DecimalInputFieldView(ViewGroup parent) : base(parent)
        {
            Initialize();
        }
        
        private void Initialize()
        {
            ValueEditText.InputType = InputTypes.ClassNumber | InputTypes.NumberFlagDecimal | InputTypes.NumberFlagSigned;
        }
    }
}