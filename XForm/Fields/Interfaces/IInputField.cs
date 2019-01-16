using System;

namespace XForm.Fields.Interfaces
{
    public interface IInputField
    {
        event EventHandler ValueTextChanged;
        
        string ValueText { get; set; }
    }
}