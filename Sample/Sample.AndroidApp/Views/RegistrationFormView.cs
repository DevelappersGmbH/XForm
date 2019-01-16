using Android.Runtime;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Sample.AndroidApp.Views.Bases;
using Sample.Core.ViewModels;

namespace Sample.AndroidApp.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register("xform.sample.androidapp.views.RegistrationFormView")]
    public class RegistrationFormView : FormFragment<RegistrationFormViewModel>
    {
        public override int Layout => Resource.Layout.Form;
    }
}