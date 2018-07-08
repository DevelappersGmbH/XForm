using XForm.Forms;

namespace XForm.Ios
{
    public class IosForm: Form
    {
        public static void Register()
        {
            FormCreateFunc = () => new IosForm();
        }
    }
}