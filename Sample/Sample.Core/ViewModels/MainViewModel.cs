using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using XForm.Fields;
using XForm.Forms;

namespace Sample.Core.ViewModels
{
    public class MainViewModel: MvxViewModel
    {
        public Form Form { get; private set; }
        
        public override async Task Initialize()
        {
            await base.Initialize();
            
            Form = Form.Create(new []
            {
                new ButtonField("Input fields", new MvxAsyncCommand(HandleNavigateToInputFieldsCommand)) 
            });
        }

        private async Task HandleNavigateToInputFieldsCommand()
        {
            await NavigationService.Navigate<InputFieldsFormViewModel>();
        }
    }
}