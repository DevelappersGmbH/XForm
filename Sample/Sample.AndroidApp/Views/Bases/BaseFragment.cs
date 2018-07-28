using Android.Content.Res;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.ViewModels;

namespace Sample.AndroidApp.Views.Bases
{
    public abstract class BaseFragment<TViewModel> : MvxFragment<TViewModel> where TViewModel : class, IMvxViewModel
    {
        private Toolbar _toolbar;
        private MvxActionBarDrawerToggle _drawerToggle;

        public abstract int Layout { get; }

        private MainView ParentActivity => (MainView) Activity;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            
            var view = this.BindingInflate(Layout, null);

            _toolbar = view.FindViewById<Toolbar>(Resource.Id.toolbar);

            if (_toolbar == null)
                return view;
            
            ParentActivity.SetSupportActionBar(_toolbar);
            ParentActivity.SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            _drawerToggle = new MvxActionBarDrawerToggle(Activity,
                                                         ParentActivity.DrawerLayout,
                                                         _toolbar,                   
                                                         Resource.String.drawer_open,
                                                         Resource.String.drawer_close);

            _drawerToggle.DrawerOpened += OnDrawerOpen;
            
            ParentActivity.DrawerLayout.AddDrawerListener(_drawerToggle);
            
            return view;
        }

        public override void OnDestroyView()
        {
            base.OnDestroyView();

            _drawerToggle.DrawerOpened -= OnDrawerOpen;
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            
            if (_toolbar != null)
                _drawerToggle.OnConfigurationChanged(newConfig);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            
            if (_toolbar != null)
                _drawerToggle.SyncState();
        }

        private void OnDrawerOpen(object sender, ActionBarDrawerEventArgs e)
        {
            ParentActivity?.HideSoftKeyboard();
        }
    }
}