using XForm.Forms;

namespace XForm.Ios.Forms
{
    public class IosForm: Form
    {
        public static void Register()
        {
            FormCreateFunc = () => new IosForm();
        }
    }
}