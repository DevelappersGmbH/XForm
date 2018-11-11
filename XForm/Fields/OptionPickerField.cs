using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using XForm.Fields.Bases;
using XForm.Fields.Interfaces;

namespace XForm.Fields
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

    public class OptionPickerField<TOption> : ValueField<int?>, IOptionPickerField<TOption>
    {
        public OptionPickerField(string title,
                                 IList<TOption> options,
                                 TOption selectedOption = default(TOption),
                                 Func<TOption, string> optionTextGetter = null) : base(title, ValueForOption(selectedOption, options))
        {
            Options = options;
            OptionTextGetter = optionTextGetter;
        }

        public TOption SelectedOption => OptionForValue(Value);

        public string SelectedOptionText => OptionTextForValue(Value);

        public int OptionsCount => Options.Count;

        public IList<TOption> Options { get; }

        public IList<string> OptionTexts => Options?.Select(OptionText).ToList();

        public Func<TOption, string> OptionTextGetter { get; }
        
        public string OptionTextForValue(int? value)
        {
            var option = OptionForValue(value);

            return OptionText(option);
        }

        private string OptionText(TOption option)
        {
            return OptionTextGetter?.Invoke(option) ?? option?.ToString();
        }

        private TOption OptionForValue(int? value)
        {
            return value.HasValue ? Options.ElementAt(value.Value) : default(TOption);
        }

        private static int? ValueForOption(TOption option, IList<TOption> options)
        {
            if (!Equals(option, default(TOption)) && !options.Contains(option))
                throw new ArgumentException("Option should be in options list or should be option's type default value!");

            var index = options.IndexOf(option);

            if (index == -1)
                return null;

            return index;
        }
    }
}