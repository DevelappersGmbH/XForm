using Android.App;
using MvvmCross.Platforms.Android.Core;
using MvvmCross.Platforms.Android.Views;
using Sample.Core;

namespace Sample.AndroidApp
{
    [Application]
    public class MainApplication : MvxAndroidApplication<MvxAndroidSetup<App>, App>
    {
    }
}