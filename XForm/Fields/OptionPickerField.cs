using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using XForm.Fields.Bases;
using XForm.Fields.Interfaces;
using XForm.Forms;

namespace XForm.Fields
{
    public class OptionPickerFieldAttribute : FieldAttribute
    {
        private readonly string _name;
        private PropertyInfo _bindedFieldProperty;

        public override PropertyInfo BindedFieldProperty => _bindedFieldProperty;

        public OptionPickerFieldAttribute(string name)
        {
            _name = name;
        }

        public override Field CreateField(FormModel formModel, PropertyInfo propertyInfo)
        {
            var propertyName = propertyInfo.Name;
            var propertyType = propertyInfo.PropertyType;
            
            // Get options
            var options = formModel.GetOptionsForField(propertyName) ?? 
                          throw new ArgumentException($"Excepted options for {propertyName}. " +
                                                      $"Please override {nameof(formModel.GetOptionsForField)}");

            // Cast options to excepted type (property type)
            var castedOptions = CastOptions(options, propertyType, out var mismatchingType);

            if (mismatchingType != null)
                throw new ArgumentException($"Excepted list of options with type {propertyType} and got {mismatchingType}. " +
                                            $"Please check returned options in {nameof(formModel.GetOptionsForField)} for propertyName {propertyName}");
            
            // Get selected option
            var selectedOption = propertyInfo.GetValue(formModel);
            
            // Create field
            var type = typeof(OptionPickerField<>).MakeGenericType(propertyType);
            var field = Activator.CreateInstance(type, _name, castedOptions, selectedOption, null) as Field;

            _bindedFieldProperty = type.GetProperty(nameof(OptionPickerField<string>.SelectedOption));
            
            return field;
        }

        private object CastOptions(IList<object> options, Type itemType, out Type mismatchingType)
        {
            mismatchingType = null;

            var optionsCount = options.Count;
            var array = Array.CreateInstance(itemType, optionsCount);
            
            for (var index = 0; index < optionsCount; index ++)
            {
                var option = options[index];
                
                try
                {
                    var castedOption = Convert.ChangeType(option, itemType);
                    array.SetValue(castedOption, index);
                }
                catch
                {
                    mismatchingType = option.GetType();
                    return null;
                }
            }
            
            return array;
        }
    }

    public class OptionPickerField<TOption> : ValueField<int?>, IOptionPickerField<TOption>
    {
        public OptionPickerField(string title,
                                 IList<TOption> options,
                                 TOption selectedOption = default(TOption),
                                 Func<TOption, string> optionTextGetter = null) : base(title, ValueForOption(selectedOption, options))
        {
            Options = options ?? new List<TOption>();
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

        public Func<TOption, string> OptionTextGetter { get; set; }

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