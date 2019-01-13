using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.Commands;
using Sample.Core.ViewModels.Bases;
using XForm.Fields;
using XForm.Fields.Interfaces;
using XForm.Forms;

namespace Sample.Core.ViewModels
{
    public class InputFieldsFormViewModel : FormViewModel
    {
        private readonly LabelField _decimalLabelField = new LabelField("Decimal", string.Empty);
        private readonly LabelField _numberLabelField = new LabelField("Number", string.Empty);
        private readonly LabelField _textLabelField = new LabelField("Text", string.Empty);
        private readonly LabelField _optionLabelField = new LabelField("Option", string.Empty);

        public override async Task Initialize()
        {
            await base.Initialize();

            var hideLabelFieldsField = new ButtonField("Hide/show label fields", new MvxCommand(() =>
            {
                _decimalLabelField.Hidden = !_decimalLabelField.Hidden;
                _numberLabelField.Hidden = !_numberLabelField.Hidden;
                _textLabelField.Hidden = !_textLabelField.Hidden;
                _optionLabelField.Hidden = !_optionLabelField.Hidden;
            }));

            var numberInputField = new NumberInputField("Number");
            numberInputField.ValueChanged += (sender, args) => _numberLabelField.Value = numberInputField.Value.ToString();

            var decimalInputField = new DecimalInputField("Decimal");
            decimalInputField.ValueChanged += (sender, args) => _decimalLabelField.Value = decimalInputField.Value.ToString();

            var textField = new SingleLineTextField("Text");
            textField.ValueChanged += (sender, args) => _textLabelField.Value = textField.Value.ToString();

            var optionField = new OptionPickerField<string>("Options", new[] {"Option 1", "Option 2", "Option 3"}, "Option 2");
            optionField.ValueChanged += (sender, args) => _optionLabelField.Value = optionField.Value.ToString();

            Form = Form.Create(new List<IField>
            {
                hideLabelFieldsField,
                _numberLabelField,
                numberInputField,
                _decimalLabelField,
                decimalInputField,
                _textLabelField,
                textField,
                _optionLabelField,
                optionField
            });
        }
    }
}