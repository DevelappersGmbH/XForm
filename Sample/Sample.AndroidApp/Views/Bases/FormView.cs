using Android.OS;
using Android.Views;
using Sample.Core.ViewModels.Bases;

namespace Sample.AndroidApp.Views.Bases
{
    public abstract class FormView<TViewModel> : BaseFragment<TViewModel> where TViewModel : FormViewModel
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            HasOptionsMenu = true;

            return view;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Layout.options_menu, menu);
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
    }
}