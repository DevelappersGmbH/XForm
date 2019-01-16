using System;
using XForm.Fields;
using XForm.Ios.FieldViews.Bases;

namespace XForm.Ios.FieldViews
{
    public class SingleLineTextFieldView : TextFieldView<SingleLineTextField>
    {
        public SingleLineTextFieldView(IntPtr handle) : base(handle)
        {
        }
    }
}