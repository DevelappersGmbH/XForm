using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Core;
using Sample.Core.Extensions;
using Sample.Core.ViewModels.Bases;
using XForm.Fields;
using XForm.Forms;

namespace Sample.Core.ViewModels
{
    public class RegistrationFormViewModel : FormViewModel
    {
        private class FormModel: XForm.Forms.FormModel
        {
            public FormModel(IMvxCommand registerCommand)
            {
                RegisterCommand = registerCommand;
            }
            
            [SingleLineTextField("First name")]
            public string FirstName { get; set; }
            
            [SingleLineTextField("Second name")]
            public string SecondName { get; set; }
            
            [EmailAddressTextField("E-Mail Address")]
            public string EmailAddress { get; set; }
            
            [PasswordTextField("Password")]
            public string Password { get; set; }

            [ButtonField("Register")]
            public IMvxCommand RegisterCommand { get; }

            protected override void FieldValueChanged()
            {
                RegisterCommand.RaiseCanExecuteChanged();
            }

            public bool IsValid()
            {
                return !string.IsNullOrWhiteSpace(FirstName) && FirstName.Length > 2 &&
                       !string.IsNullOrWhiteSpace(SecondName) && SecondName.Length > 2 &&
                       EmailAddress.IsValidEmailAddress() &&
                       Password.IsSavePassword();
            }
        }
        
        private FormModel _formModel;
        private IMvxCommand _registerCommand;

        private IMvxCommand RegisterCommand => _registerCommand ?? (_registerCommand = new MvxAsyncCommand(HandleRegisterCommand,
                                                                                                           CanHandleRegisterCommand));

        public override async Task Initialize()
        {
            await base.Initialize();
            
            _formModel = new FormModel(RegisterCommand);
            Form = Form.Create(_formModel);
        }

        private bool CanHandleRegisterCommand()
        {
            return _formModel.IsValid();
        }

        private async Task HandleRegisterCommand()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
        }
    }
}