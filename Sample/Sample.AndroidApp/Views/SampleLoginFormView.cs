using Android.Runtime;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Sample.AndroidApp.Views.Bases;
using Sample.Core.ViewModels;

namespace Sample.AndroidApp.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register("xform.sample.androidapp.views.SampleLoginFormView")]
    public class SampleLoginFormView : FormFragment<SampleLoginFormViewModel>
    {
        public override int Layout => Resource.Layout.Form;
    }
}