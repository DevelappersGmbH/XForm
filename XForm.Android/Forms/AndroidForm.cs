using XForm.Forms;

namespace XForm.Android.Forms
{
    public class AndroidForm: Form
    {
        public static void Register()
        {
            Form.FormCreateFunc = () => new AndroidForm();
        }
    }
}