using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XForm.Binding
{
    public abstract class BindableBase: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected bool Set<T>(ref T field, T newValue, EventHandler customChangedEventHandler = null, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }

            field = newValue;

            RaisePropertyChanged(propertyName);
            customChangedEventHandler?.Invoke(this, new EventArgs());
            
            return true;
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            OnPropertyChanged(propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
        }
    }
}