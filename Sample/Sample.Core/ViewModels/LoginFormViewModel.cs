using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using Sample.Core.Extensions;
using Sample.Core.ViewModels.Bases;
using XForm.Fields;
using XForm.Forms;

namespace Sample.Core.ViewModels
{
    public class LoginFormViewModel : FormViewModel
    {
        private class FormModel: XForm.Forms.FormModel
        {
            public FormModel(IMvxCommand loginCommand)
            {
                LoginCommand = loginCommand;
            }

            [EmailAddressTextField("E-Mail Address")]
            public string EmailAddress { get; set; }

            [PasswordTextField("Password")]
            public string Password { get; set; }

            [ButtonField("Login")]
            public IMvxCommand LoginCommand { get; }

            protected override void FieldValueChanged()
            {
                LoginCommand.RaiseCanExecuteChanged();
            }

            public bool IsValid()
            {
                return EmailAddress.IsValidEmailAddress() &&
                       Password.IsSavePassword();
            }
        }

        private FormModel _formModel;
        private IMvxCommand _loginCommand;

        private IMvxCommand LoginCommand => _loginCommand ?? (_loginCommand = new MvxAsyncCommand(HandleLoginCommand,
                                                                                                  CanHandleLoginCommand));

        public override async Task Initialize()
        {
            await base.Initialize();

            _formModel = new FormModel(LoginCommand);
            
            Form = Form.Create(_formModel);
        }

        private bool CanHandleLoginCommand()
        {
            return _formModel.IsValid();
        }

        private async Task HandleLoginCommand()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));

            _formModel.EmailAddress = string.Empty;
            _formModel.Password = string.Empty;
        }
    }
}