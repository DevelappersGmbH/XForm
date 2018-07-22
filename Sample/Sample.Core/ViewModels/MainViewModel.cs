using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Sample.Core.ViewModels
{
    public class MainViewModel: MvxViewModel
    {
        public IMvxCommand ShowContentViewModelsCommand => new MvxCommand(HandleShowContentViewModelsCommand);

        private void HandleShowContentViewModelsCommand()
        {
            
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            
            NavigationService.Navigate<InputFieldsFormViewModel>();
            NavigationService.Navigate<MenuFormViewModel>();
        }
    }
}