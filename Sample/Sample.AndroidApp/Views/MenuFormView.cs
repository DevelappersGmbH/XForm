using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Sample.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using Android.Runtime;
using Android.Views;
using Android.OS;

namespace Sample.AndroidApp.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.navigation_frame)]
    [Register("xform.sample.androidapp.views.MenuFormView")]
    public class MenuFormView : MvxFragment<MenuFormViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(Resource.Layout.Menu, null);
        }
    }
}