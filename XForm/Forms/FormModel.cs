using System;
using System.Collections.Generic;
using XForm.Binding;
using XForm.FieldAttributes;
using XForm.Fields;
using XForm.Fields.Bases;
using XForm.Fields.Interfaces;

namespace XForm.Forms
{
    public abstract class FormModel : BindableBase
    {
        private readonly List<Binding.Binding> _bindings = new List<Binding.Binding>();

        /// <summary>
        /// Is called when the value of a field has changed.
        /// </summary>
        protected virtual void FieldValueChanged()
        {
        }

        /// <summary>
        /// Provide the options for all properties with <see cref="OptionPickerFieldAttribute"/> of the model.
        /// </summary>
        /// <param name="propertyName">Name of requested property</param>
        /// <returns>List of options. Options should have the same type as the requested property.</returns>
        public virtual IList<object> GetOptionsForField(string propertyName)
        {
            return null;
        }

        internal List<Field> CreateAndBindFields()
        {
            var properties = GetType().GetProperties();

            var fields = new List<Field>();

            foreach (var property in properties)
            {
                var attribute = Attribute.GetCustomAttribute(property, typeof(FieldAttribute), true);

                if (attribute == null)
                    continue;

                var fieldAttribute = (FieldAttribute) attribute;
                var field = fieldAttribute.CreateField(this, property);

                var binding = new Binding.Binding(field, fieldAttribute.BindedFieldProperty,
                                                  this, property);

                binding.SetOwnerFromTarget();
                binding.Bind();

                _bindings.Add(binding);

                if (field is IValueField valueField)
                    valueField.ValueChanged += ValueChanged;

                fields.Add(field);
            }

            return fields;
        }

        private void ValueChanged(object sender, EventArgs args)
        {
            FieldValueChanged();
        }
    }
}