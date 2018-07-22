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
                new ButtonField("Sample login", new MvxAsyncCommand(HandleNavigateToSampleLoginCommand)) 
            });
        }

        private async Task HandleNavigateToSampleLoginCommand()
        {
            await NavigationService.Navigate<SampleLoginFormViewModel>();
        }

        private async Task HandleNavigateToInputFieldsCommand()
        {
            await NavigationService.Navigate<InputFieldsFormViewModel>();
        }
    }
}