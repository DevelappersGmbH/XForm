using System;
using UIKit;
using XForm.Fields;
using XForm.Ios.FieldViews.Bases;

namespace XForm.Ios.FieldViews
{
    public class NumberInputFieldView : InputFieldView<NumberInputField, int?>
    {
        public NumberInputFieldView(IntPtr handle) : base(handle)
        {
            ValueTextField.KeyboardType = UIKeyboardType.NumberPad;
        }
    }
}