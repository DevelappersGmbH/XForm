using MvvmCross.ViewModels;
using Sample.Core.ViewModels;

namespace Sample.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<MainViewModel>();
        }
    }
}