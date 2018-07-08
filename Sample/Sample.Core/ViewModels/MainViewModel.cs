using System.Threading.Tasks;
using MvvmCross.ViewModels;

namespace Sample.Core.ViewModels
{
    public class MainViewModel: MvxViewModel
    {
        public override async Task Initialize()
        {
            await base.Initialize();
        }
    }
}