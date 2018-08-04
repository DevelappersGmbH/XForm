using Android.OS;
using Android.Views;
using MvvmCross.Binding.BindingContext;
using Sample.Core.ViewModels.Bases;

namespace Sample.AndroidApp.Views.Bases
{
    public abstract class FormView<TViewModel> : BaseFragment<TViewModel> where TViewModel : FormViewModel
    {
        private bool _formEnabled;

        public bool FormEnabled
        {
            get => _formEnabled;
            set
            {
                if (_formEnabled == value)
                    return;

                _formEnabled = value;
                Activity.InvalidateOptionsMenu();
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            HasOptionsMenu = true;

            var set = this.CreateBindingSet<FormView<TViewModel>, TViewModel>();
            set.Bind(this).For(v => v.FormEnabled).To(vm => vm.Form.Enabled);
            set.Apply();

            return view;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.options_menu, menu);
        }

        public override void OnPrepareOptionsMenu(IMenu menu)
        {
            base.OnPrepareOptionsMenu(menu);
            UpdateToggleEnabledIcon(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.toggle_enabled:
                    ViewModel.FormEnableToggleCommand.Execute();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private void UpdateToggleEnabledIcon(IMenu menu)
        {
            var item = menu.FindItem(Resource.Id.toggle_enabled);

            var iconId = FormEnabled ? Resource.Drawable.icon_unlock : Resource.Drawable.icon_lock;
            var icon = Context.GetDrawable(iconId);
            
            item.SetIcon(icon);
        }
    }
}