using Foundation;
using MvvmCross.Platforms.Ios.Core;
using Sample.Core;
using UIKit;
using XForm.Ios.Forms;

namespace Sample.IosApp
{
    [Register("AppDelegate")]
    public class AppDelegate : MvxApplicationDelegate<Setup, App>
    {
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            IosForm.Register();
            
            return base.FinishedLaunching(application, launchOptions);
        }
    }
}

