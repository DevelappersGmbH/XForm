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
    public class SampleLoginFormViewModel : FormViewModel
    {
        private IMvxCommand _loginCommand;

        private readonly EmailAddressTextField _emailAddressField = new EmailAddressTextField("E-Mail Address");
        private readonly PasswordTextField _passwordField = new PasswordTextField("Password");
        private ButtonField _buttonField;

        private IMvxCommand LoginCommand =>
            _loginCommand ?? (_loginCommand = new MvxAsyncCommand(HandleLoginCommand, CanHandleLoginCommand));

        public override async Task Initialize()
        {
            await base.Initialize();

            _buttonField = new ButtonField("Login", LoginCommand);

            _emailAddressField.ValueChanged += (sender, args) => LoginCommand.RaiseCanExecuteChanged();
            _passwordField.ValueChanged += (sender, args) => LoginCommand.RaiseCanExecuteChanged();

            Form = Form.Create(new IField[]
            {
                _emailAddressField,
                _passwordField,
                _buttonField
            });
        }

        private bool CanHandleLoginCommand()
        {
            return _emailAddressField.Value.IsValidEmailAddress()
                   && _passwordField.Value.IsSavePassword();
        }

        private async Task HandleLoginCommand()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));

            _emailAddressField.Value = string.Empty;
            _passwordField.Value = string.Empty;
        }
    }
}