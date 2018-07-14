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
        public Form Form { get; private set; }
        
        public override async Task Initialize()
        {
            await base.Initialize();
            
            Form = Form.Create(new List<IField>
            {
                new LabelField("title", "value"),
                new LabelField("title 1", "value 1"),
                new LabelField("title 2", "value 2"),
                new LabelField("title title title title", "value value value value value"),
                new ButtonField("Insert label", new MvxCommand(HandleInsertLabelCommand))
            });
        }

        private void HandleInsertLabelCommand()
        {
            Form.InsertField(2, new LabelField("New field", "Yeah an new field"));
        }
    }
}