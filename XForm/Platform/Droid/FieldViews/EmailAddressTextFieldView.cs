using Android.Text;
using Android.Views;

namespace XForm.Droid.FieldViews
{
    public class EmailAddressTextFieldView : SingleLineTextFieldView
    {
        public EmailAddressTextFieldView(ViewGroup parent) : base(parent)
        {
        }

        public EmailAddressTextFieldView(ViewGroup parent, int layoutToInflate) : base(parent, layoutToInflate)
        {
        }

        internal override void Setup()
        {
            base.Setup();

            ValueEditText.InputType = InputTypes.ClassText | InputTypes.TextVariationWebEmailAddress;
        }
    }
    
    public class PasswordTextFieldView : SingleLineTextFieldView
    {
        public PasswordTextFieldView(ViewGroup parent) : base(parent)
        {
        }

        public PasswordTextFieldView(ViewGroup parent, int layoutToInflate) : base(parent, layoutToInflate)
        {
        }

        internal override void Setup()
        {
            base.Setup();

            ValueEditText.InputType = InputTypes.ClassText | InputTypes.TextVariationWebPassword;
        }
    }
}