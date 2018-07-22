using System.Threading.Tasks;
using Sample.Core.ViewModels.Bases;
using XForm.Fields;
using XForm.Forms;

namespace Sample.Core.ViewModels
{
    public class SampleLoginFormViewModel : FormViewModel
    {
        public override async Task Initialize()
        {
            await base.Initialize();
            
            Form = Form.Create(new []
            {
                new LabelField("Coming soon", string.Empty) 
            });
        }
    }
}