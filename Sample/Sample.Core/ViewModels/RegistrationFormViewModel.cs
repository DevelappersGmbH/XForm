using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using Sample.Core.Extensions;
using Sample.Core.ViewModels.Bases;
using XForm.Fields;
using XForm.Fields.Bases;
using XForm.Forms;

namespace Sample.Core.ViewModels
{
    public class RegistrationFormViewModel : FormViewModel
    {
        private readonly SingleLineTextField _firstNameField = new SingleLineTextField("First name");
        private readonly SingleLineTextField _secondNameField = new SingleLineTextField("Second name");
        private readonly EmailAddressTextField _emailAddressField = new EmailAddressTextField("E-Mail address");
        private readonly PasswordTextField _passwordField = new PasswordTextField("Password");

        private IMvxCommand _registerCommand;

        private IMvxCommand RegisterCommand => _registerCommand ?? (_registerCommand = new MvxAsyncCommand(HandleRegisterCommand,
                                                                                                           CanHandleRegisterCommand));

        public override async Task Initialize()
        {
            await base.Initialize();

            _firstNameField.ValueChanged += (sender, args) => RegisterCommand.RaiseCanExecuteChanged();
            _secondNameField.ValueChanged += (sender, args) => RegisterCommand.RaiseCanExecuteChanged();
            _emailAddressField.ValueChanged += (sender, args) => RegisterCommand.RaiseCanExecuteChanged();
            _passwordField.ValueChanged += (sender, args) => RegisterCommand.RaiseCanExecuteChanged();

            Form = Form.Create(new Field[]
            {
                _firstNameField,
                _secondNameField,
                _emailAddressField,
                _passwordField,
                new ButtonField("Register", RegisterCommand)
            });
        }

        private bool CanHandleRegisterCommand()
        {
            return !string.IsNullOrWhiteSpace(_firstNameField.Value) && _firstNameField.Value.Length > 2 &&
                   !string.IsNullOrWhiteSpace(_secondNameField.Value) && _secondNameField.Value.Length > 2 &&
                   _emailAddressField.Value.IsValidEmailAddress() &&
                   _passwordField.Value.IsSavePassword();
        }

        private async Task HandleRegisterCommand()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            
            _firstNameField.Value = string.Empty;
            _secondNameField.Value = string.Empty;
            _emailAddressField.Value = string.Empty;
            _passwordField.Value = string.Empty;
        }
    }
}