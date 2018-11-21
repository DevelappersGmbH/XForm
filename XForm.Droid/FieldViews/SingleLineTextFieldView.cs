using Android.Views;
using XForm.Droid.FieldViews.Bases;
using XForm.Fields;

namespace XForm.Droid.FieldViews
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
