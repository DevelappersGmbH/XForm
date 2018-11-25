using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using Sample.Core.Extensions;
using Sample.Core.ViewModels.Bases;
using XForm.Fields;
using XForm.Fields.Interfaces;
using XForm.Forms;

namespace Sample.Core.ViewModels
{
    public class LoginFormViewModel : FormViewModel
    {
        private class FormModel: XForm.Forms.FormModel
        {
            private string _emailAddress;
            private string _password;

            public FormModel(IMvxCommand loginCommand)
            {
                LoginCommand = loginCommand;
            }

            [EmailAddressTextField("E-Mail Address")]
            public string EmailAddress
            {
                get => _emailAddress;
                set => Set(ref _emailAddress, value);
            }

            [PasswordTextField("Password")]
            public string Password
            {
                get => _password;
                set => Set(ref _password, value);
            }

            [ButtonField("Login")]
            public IMvxCommand LoginCommand { get; }
            
            public override void FieldValueChanged()
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