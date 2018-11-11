using XForm.Android.FieldViews;
using XForm.Fields;
using XForm.Forms;

namespace XForm.Android.Forms
{
    public class AndroidForm: Form
    {
        public static void Register()
        {
            FormCreateFunc = () => new AndroidForm();
        }

        protected override void RegisterFieldViews(FieldViewLocator locator)
        {
            base.RegisterFieldViews(locator);
            
            locator.Register<LabelField, LabelFieldView>();
            locator.Register<ButtonField, ButtonFieldView>();
            
            locator.Register<SingleLineTextField, SingleLineTextFieldView>();
            
            locator.Register<DecimalInputField, DecimalInputFieldView>();
            locator.Register<NumberInputField, NumberInputFieldView>();
            
            locator.Register<IOptionPickerField, OptionPickerFieldView>();
        }
    }
}