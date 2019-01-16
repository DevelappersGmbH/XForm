using System.Threading.Tasks;
using MvvmCross.Commands;
using Sample.Core.ViewModels.Bases;
using XForm.Fields;
using XForm.Forms;

namespace Sample.Core.ViewModels
{
    public class MenuFormViewModel : FormViewModel
    {
        public override async Task Initialize()
        {
            await base.Initialize();
            
            Form = Form.Create(new []
            {
                new ButtonField("Input fields", new MvxAsyncCommand(HandleNavigateToInputFieldsCommand)),
                new ButtonField("Sample login", new MvxAsyncCommand(HandleNavigateToLoginCommand)),
                new ButtonField("Sample registration", new MvxAsyncCommand(HandleNavigateToRegistrationCommand)),
                new ButtonField("Custom design", new MvxAsyncCommand(HandleNavigateToCustomDesignCommand)) 
            });
        }

        private async Task HandleNavigateToLoginCommand()
        {
            await NavigationService.Navigate<LoginFormViewModel>();
        }
        
        private async Task HandleNavigateToRegistrationCommand()
        {
            await NavigationService.Navigate<RegistrationFormViewModel>();
        }

        private async Task HandleNavigateToInputFieldsCommand()
        {
            await NavigationService.Navigate<InputFieldsFormViewModel>();
        }

        private async Task HandleNavigateToCustomDesignCommand()
        {
            await NavigationService.Navigate<CustomDesignFormViewModel>();
        }
    }
}