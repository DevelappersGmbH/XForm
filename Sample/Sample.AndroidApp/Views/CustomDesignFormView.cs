using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Sample.AndroidApp.Views.Bases;
using Sample.Core.ViewModels;
using XForm.Droid.FieldViews;

namespace Sample.AndroidApp.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register("xform.sample.androidapp.views.CustomDesignFormView")]
    public class CustomDesignFormView : FormFragment<CustomDesignFormViewModel>
    {
        public override int Layout => Resource.Layout.Form;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            FormView.CreateView<SingleLineTextFieldView>(parent => new SingleLineTextFieldView(parent, Resource.Layout.EditTextFieldView));
            
            return view;
        }
    }
}