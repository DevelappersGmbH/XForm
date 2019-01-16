using MvvmCross.Binding.BindingContext;
using MvvmCross.Plugin.Sidebar;
using Sample.Core.ViewModels;
using Sample.IosApp.Views.Bases;

namespace Sample.IosApp.Views
{
    [MvxSidebarPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true, MvxSplitViewBehaviour.Detail)]
    public partial class InputFieldsFormView : FormView<InputFieldsFormViewModel>
    {
        public InputFieldsFormView() : base("InputFieldsFormView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<InputFieldsFormView, InputFieldsFormViewModel>();

            set.Bind(FormView).For(v => v.Form).To(vm => vm.Form);
            
            set.Apply();
        }
    }
}

