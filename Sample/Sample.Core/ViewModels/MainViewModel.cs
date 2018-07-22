using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Sample.Core.ViewModels
{
    public class MainViewModel: MvxViewModel
    {
        public IMvxCommand ShowContentViewModelsCommand => new MvxCommand(HandleShowContentViewModelsCommand);

        private void HandleShowContentViewModelsCommand()
        {
            NavigationService.Navigate<MenuFormViewModel>();
            NavigationService.Navigate<InputFieldsFormViewModel>();
        }
    }
}