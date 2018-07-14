using XForm.Fields;
using XForm.Forms;
using XForm.Ios.FieldViews;

namespace XForm.Ios.Forms
{
    public class IosForm: Form
    {
        public static void Register()
        {
            FormCreateFunc = () => new IosForm();
        }

        protected override void RegisterFieldViews(FieldViewLocator locator)
        {
            base.RegisterFieldViews(locator);
            
            locator.Register<LabelField, LabelFieldView>();
            locator.Register<ButtonField, ButtonFieldView>();
            
            locator.Register<TextInputField, TextInputFieldView>();
        }
    }
}