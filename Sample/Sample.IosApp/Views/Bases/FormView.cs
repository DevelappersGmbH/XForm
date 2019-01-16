using System;
using System.Globalization;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Converters;
using MvvmCross.Platforms.Ios.Views;
using Sample.Core.ViewModels.Bases;
using UIKit;

namespace Sample.IosApp.Views.Bases
{
    public class LockedImageConverter : MvxValueConverter<bool, UIImage>
    {
        protected override UIImage Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return UIImage.FromBundle(value ? "IconUnlock" : "IconLock");
        }
    }
    
    public class FormView<TViewModel> : MvxViewController<TViewModel> where TViewModel : FormViewModel
    {
        private readonly UIBarButtonItem _toggleEnabledBarButtonItem = new UIBarButtonItem(UIImage.FromBundle("IconLock"), UIBarButtonItemStyle.Plain, null);

        public FormView(IntPtr handle) : base(handle)
        {
        }

        protected FormView(string nibName, NSBundle bundle) : base(nibName, bundle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationItem.RightBarButtonItem = _toggleEnabledBarButtonItem;

            var set = this.CreateBindingSet<FormView<TViewModel>, TViewModel>();

            set.Bind(_toggleEnabledBarButtonItem).For(v => v.Image).To(vm => vm.Form.Enabled).WithConversion<LockedImageConverter>();
            set.Bind(_toggleEnabledBarButtonItem).To(vm => vm.FormEnableToggleCommand);
            
            set.Apply();
        }
    }
}