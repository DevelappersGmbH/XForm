using System;
using System.Windows.Input;
using XForm.EventSubscription;
using XForm.Fields.Bases;

namespace XForm.Fields
{
    public class ButtonField : ValueField<ICommand>
    {
        private IDisposable _canExecuteChangedSubscription;
        
        public ButtonField(string title, ICommand value) : base(title, value)
        {
        }

        protected override void HandleValueChanged(ICommand oldValue, ICommand newValue)
        {
            base.HandleValueChanged(oldValue, newValue);
            
            _canExecuteChangedSubscription?.Dispose();
            _canExecuteChangedSubscription = newValue?.WeakSubscribe(HandleCanExecuteChanged);
            
            HandleCanExecuteChanged(newValue, null);
        }

        private void HandleCanExecuteChanged(object sender, EventArgs e)
        {
            Enabled = Value?.CanExecute(null) ?? false;
        }
    }
}