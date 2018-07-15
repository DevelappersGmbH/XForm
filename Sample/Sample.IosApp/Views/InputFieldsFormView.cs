using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using Sample.Core.ViewModels;

namespace Sample.IosApp.Views
{
    [MvxChildPresentation]
    public partial class InputFieldsFormView : MvxViewController<InputFieldsFormViewModel>
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

