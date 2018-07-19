using Android.Views;
using XForm.Android.FieldViews.Bases;
using XForm.Fields;

namespace XForm.Android.FieldViews
{
    public class SingleLineTextFieldView : TextFieldView<SingleLineTextField>
    {
        public SingleLineTextFieldView(ViewGroup parent) : base(parent)
        {
            Initialize();
        }

        public SingleLineTextFieldView(ViewGroup parent, int layoutToInflate) : base(parent, layoutToInflate)
        {
            Initialize();
        }

        private void Initialize()
        {
            ValueEditText.SetMaxLines(1);
            ValueEditText.SetSingleLine(true);
        }
    }
}
