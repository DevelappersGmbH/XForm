using Android.App;
using Android.OS;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views;
using Sample.Core.ViewModels;

namespace Sample.AndroidApp.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "MainView", MainLauncher = true)]
    public class MainView : MvxActivity<MainViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.Main);
        }
    }
}
