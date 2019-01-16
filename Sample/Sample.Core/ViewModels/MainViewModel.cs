using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Sample.Core.ViewModels
{
    public class MainViewModel: MvxViewModel
    {
        public IMvxCommand NavigateToMenuViewModelCommand => new MvxCommand(HandleNavigateToMenuViewModelCommand);

        private void HandleNavigateToMenuViewModelCommand()
        {
            NavigationService.Navigate<MenuFormViewModel>();
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            
            NavigationService.Navigate<InputFieldsFormViewModel>();
        }
    }
}