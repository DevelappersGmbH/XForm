using Android.Text;
using Android.Views;
using XForm.Droid.FieldViews.Bases;
using XForm.Fields;

namespace XForm.Droid.FieldViews
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