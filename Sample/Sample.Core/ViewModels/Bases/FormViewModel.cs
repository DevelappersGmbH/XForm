using MvvmCross.ViewModels;
using XForm.Forms;

namespace Sample.Core.ViewModels.Bases
{
    public abstract class FormViewModel : MvxViewModel
    {
        private Form _form;

        public Form Form
        {
            get => _form;
            protected set => SetProperty(ref _form, value);
        }
    }
}