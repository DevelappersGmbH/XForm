using System;
using System.Windows.Input;

namespace XForm.Tests.Mocks
{
    public class MockCommand : ICommand
    {
        private bool _enabled;

        public bool Enabled
        {
            get => _enabled;
            set
            {
                if (Equals(_enabled, value))
                    return;

                _enabled = value;
                CanExecuteChanged?.Invoke(this, null);
            }
        }

        public bool CanExecute(object parameter)
        {
            return Enabled;
        }

        public void Execute(object parameter)
        {
        }

        public event EventHandler CanExecuteChanged;
    }
}