using System;
using System.Collections.Generic;
using System.Linq;
using XForm.Fields.Bases;
using XForm.Fields.Interfaces;
using XForm.Forms;

namespace XForm.Fields
{
    // TODO: Provide attributes for more types or an alternative handling
    public class StringOptionPickerFieldAttribute : FieldAttribute
    {
        public StringOptionPickerFieldAttribute(string name, string[] options)
            : base(() => new OptionPickerField<string>(name, options),
                   typeof(OptionPickerField<string>).GetProperty(nameof(OptionPickerField<string>.SelectedOption)))
        {
        }
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

        public TOption SelectedOption
        {
            get => OptionForValue(Value);
            set => Value = ValueForOption(value, Options);
        }

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

        protected override void HandleValueChanged(int? oldValue, int? newValue)
        {
            base.HandleValueChanged(oldValue, newValue);
            
            RaisePropertyChanged(nameof(SelectedOption));
        }
    }
}