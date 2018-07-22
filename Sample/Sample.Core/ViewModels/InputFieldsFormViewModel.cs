using System.Collections.Generic;
using System.Threading.Tasks;
using Sample.Core.ViewModels.Bases;
using XForm.Fields;
using XForm.Fields.Interfaces;
using XForm.Forms;

namespace Sample.Core.ViewModels
{
    public class InputFieldsFormViewModel : FormViewModel
    {
        public override async Task Initialize()
        {
            await base.Initialize();
            
            var numberLabelField = new LabelField("Number", string.Empty);
            var numberInputField = new NumberInputField("Number");
            numberInputField.ValueChanged += (sender, args) => numberLabelField.Value = numberInputField.Value.ToString();
            
            var decimalLabelField = new LabelField("Decimal", string.Empty);
            var decimalInputField = new DecimalInputField("Decimal");
            decimalInputField.ValueChanged += (sender, args) => decimalLabelField.Value = decimalInputField.Value.ToString();
            
            var textLabelField = new LabelField("Text", string.Empty);
            var textField = new SingleLineTextField("Text");
            textField.ValueChanged += (sender, args) => textLabelField.Value = textField.Value.ToString();
            
            Form = Form.Create(new List<IField>
            {
                numberLabelField,
                numberInputField,
                decimalLabelField,
                decimalInputField,
                textLabelField,
                textField
            });
        }
    }
}