using Foundation;
using MvvmCross.Platforms.Ios.Core;
using Sample.Core;

namespace Sample.IosApp
{
    [Register("AppDelegate")]
    public class AppDelegate : MvxApplicationDelegate<Setup, App>
    {
    }
}

