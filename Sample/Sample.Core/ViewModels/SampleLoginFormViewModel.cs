using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using Sample.Core.ViewModels.Bases;
using XForm.Fields;
using XForm.Fields.Interfaces;
using XForm.Forms;

namespace Sample.Core.ViewModels
{
    public class SampleLoginFormViewModel : FormViewModel
    {
        public override async Task Initialize()
        {
            await base.Initialize();
            
            Form = Form.Create(new IField[]
            {
                new LabelField("Coming soon", string.Empty),
                new ButtonField("Login", new MvxAsyncCommand(HandleLoginCommand)) 
            });
        }

        private async Task HandleLoginCommand()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
        }
    }
}