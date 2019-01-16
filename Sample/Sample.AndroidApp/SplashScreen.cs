using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Sample.Core;

namespace Sample.AndroidApp
{
    [MvxActivityPresentation]
    [Activity(
        MainLauncher = true,
        Icon = "@mipmap/icon",
        Theme = "@style/Theme.Splash",
        NoHistory = true,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenAppCompatActivity<Setup, App>
    {
        public SplashScreen() : base(Resource.Layout.SplashScreen)
        {
        }
    }
}