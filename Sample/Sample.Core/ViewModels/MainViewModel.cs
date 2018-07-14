using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using XForm.Fields;
using XForm.Fields.Interfaces;
using XForm.Forms;

namespace Sample.Core.ViewModels
{
    public class MainViewModel: MvxViewModel
    {
        private readonly LabelField _sizingLabelField = new LabelField("Value", "Text"); 
        private readonly TextInputField _textInputField = new TextInputField("Text"); 
        
        public Form Form { get; private set; }
        
        public override async Task Initialize()
        {
            await base.Initialize();
            
            var sizingLabelField = new LabelField("ValueChanged", "Text");

            _textInputField.ValueChanged += (sender, args) => sizingLabelField.Value = _textInputField.Value; 
            
            Form = Form.Create(new List<IField>
            {
                sizingLabelField,
                _sizingLabelField,
                _textInputField,
                new ButtonField("Set text", new MvxCommand(HandleInsertLabelCommand))
            });
        }

        private void HandleInsertLabelCommand()
        {
            _sizingLabelField.Value = _textInputField.Value;
        }
    }
}