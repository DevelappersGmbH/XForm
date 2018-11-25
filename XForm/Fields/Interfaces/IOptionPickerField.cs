using System.Collections.Generic;

namespace XForm.Fields.Interfaces
{
    public interface IOptionPickerField : IValueField<int?>
    {
        int OptionsCount { get; }
        
        string SelectedOptionText { get; }

        string OptionTextForValue(int? value);
        
        IList<string> OptionTexts { get; }
    }
    
    public interface IOptionPickerField<TOption> : IOptionPickerField
    {
        TOption SelectedOption { get; }

        IList<TOption> Options { get; }
    }
}