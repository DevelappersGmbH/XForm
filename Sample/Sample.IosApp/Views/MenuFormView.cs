using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.Plugin.Sidebar;
using Sample.Core.ViewModels;

namespace Sample.IosApp.Views
{
    [MvxSidebarPresentation(MvxPanelEnum.Left, MvxPanelHintType.PushPanel, false)] 
    public partial class MenuFormView : MvxViewController<MenuFormViewModel>
    {
        public MenuFormView() : base("MenuFormView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<MenuFormView, MenuFormViewModel>();

            set.Bind(FormView).For(v => v.Form).To(vm => vm.Form);
            
            set.Apply();
        }
    }
}