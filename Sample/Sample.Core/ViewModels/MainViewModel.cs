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
        private readonly LabelField _sizingLabelField = new LabelField("Sizing Label", "Text"); 
        private readonly LabelField _sizingLabelField2 = new LabelField("Sizing Label", "Text"); 
        
        public Form Form { get; private set; }
        
        public override async Task Initialize()
        {
            await base.Initialize();
            
            Form = Form.Create(new List<IField>
            {
                _sizingLabelField,
                new LabelField("title", "value"),
                new LabelField("title 1", "value 1"),
                new LabelField("title 2", "value 2 value 2 value 2 value 2 value 2 value 2 value 2 value 2"),
                new LabelField("title 2", "value 2 value 2 value 2 value 2 value 2 value 2 value 2 value 2"),
                new LabelField("title 2", "value 2 value 2 value 2 value 2 value 2 value 2 value 2 value 2"),
                new LabelField("title 2", "value 2 value 2 value 2 value 2 value 2 value 2 value 2 value 2"),
                new LabelField("title 2", "value 2 value 2 value 2 value 2 value 2 value 2 value 2 value 2"),
                new LabelField("title 2", "value 2 value 2 value 2 value 2 value 2 value 2 value 2 value 2"),
                new LabelField("title 2", "value 2 value 2 value 2 value 2 value 2 value 2 value 2 value 2"),
                new LabelField("title 2", "value 2 value 2 value 2 value 2 value 2 value 2 value 2 value 2"),
                new LabelField("title 2", "value 2 value 2 value 2 value 2 value 2 value 2 value 2 value 2"),
                new LabelField("title 2", "value 2 value 2 value 2 value 2 value 2 value 2 value 2 value 2"),
                _sizingLabelField2,
                new ButtonField("Add text", new MvxCommand(HandleInsertLabelCommand))
            });
        }

        private void HandleInsertLabelCommand()
        {
            _sizingLabelField.Value += " Text";
            _sizingLabelField2.Value += " Text";
        }
    }
}