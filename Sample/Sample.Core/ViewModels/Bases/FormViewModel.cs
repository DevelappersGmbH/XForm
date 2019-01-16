using MvvmCross.Commands;
using MvvmCross.ViewModels;
using XForm.Forms;

namespace Sample.Core.ViewModels.Bases
{
    public abstract class FormViewModel : MvxViewModel
    {
        private Form _form;
        private string _formToggleTitle;

        public string FormEnableToggleTitle
        {
            get => _formToggleTitle;
            set => SetProperty(ref _formToggleTitle, value);
        }
        
        public Form Form
        {
            get => _form;
            protected set => SetProperty(ref _form, value);
        }

        public IMvxCommand FormEnableToggleCommand => new MvxCommand(HandleFromEnableToggleCommand);
        
        private void HandleFromEnableToggleCommand()
        {
            Form.Enabled = !Form.Enabled;
        }
    }
}