using System;
using UIKit;
using XForm.Fields;
using XForm.Ios.FieldViews.Bases;

namespace XForm.Ios.FieldViews
{
    public class DecimalInputFieldView : InputFieldView<DecimalInputField, double?>
    {
        public DecimalInputFieldView(IntPtr handle) : base(handle)
        {
            ValueTextField.KeyboardType = UIKeyboardType.DecimalPad;
        }
    }
}