using System;
using System.Windows.Input;
using XForm.Fields.Bases;

namespace XForm.Fields
{
    public class ButtonField : ValueField<ICommand>
    {
        public ButtonField(string title, ICommand value) : base(title, value)
        {
        }

        protected override void HandleValueChanged(ICommand oldValue, ICommand newValue)
        {
            base.HandleValueChanged(oldValue, newValue);

            if (oldValue != null) 
                oldValue.CanExecuteChanged -= HandleCanExecuteChanged;

            if (newValue != null) 
                newValue.CanExecuteChanged += HandleCanExecuteChanged;
            
            HandleCanExecuteChanged(newValue, null);
        }

        private void HandleCanExecuteChanged(object sender, EventArgs e)
        {
            Enabled = Value?.CanExecute(null) ?? false;
        }
    }
}