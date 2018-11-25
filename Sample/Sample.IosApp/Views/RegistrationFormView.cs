using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Sample.Core.ViewModels;

namespace Sample.IosApp.Views
{
    public partial class RegistrationFormView : MvxViewController<RegistrationFormViewModel>
    {
        public RegistrationFormView() : base("RegistrationFormView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<RegistrationFormView, RegistrationFormViewModel>();

            set.Bind(FormView).For(v => v.Form).To(vm => vm.Form);

            set.Apply();
        }
    }
}

