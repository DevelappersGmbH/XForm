using System.Threading.Tasks;
using Sample.Core.ViewModels.Bases;
using XForm.Fields;
using XForm.Fields.Interfaces;
using XForm.Forms;

namespace Sample.Core.ViewModels
{
    public class CustomDesignFormViewModel: FormViewModel
    {
        public override async Task Initialize()
        {
            await base.Initialize();
            
            Form = Form.Create(new IField[]
            {
                new LabelField("Just an custom design example", string.Empty),
                new SingleLineTextField("Custom Textfield", "123"), 
                new ButtonField("Custom button", null) 
            });
        }
    }
}