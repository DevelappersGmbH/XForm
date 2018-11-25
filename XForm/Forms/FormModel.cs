using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using XForm.Binding;
using XForm.Fields;
using XForm.Fields.Bases;
using XForm.Fields.Interfaces;

namespace XForm.Forms
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class FieldAttribute : Attribute
    {
        public Func<Field> FieldCreator { get; }

        public PropertyInfo BindedFieldProperty { get; }

        public FieldAttribute(Func<Field> fieldCreator, PropertyInfo bindedFieldProperty)
        {
            FieldCreator = fieldCreator;
            BindedFieldProperty = bindedFieldProperty;
        }
    }


    

    public class Binding
    {
        public INotifyPropertyChanged Owner { get; }
        public PropertyInfo OwnerProperty { get; }

        public INotifyPropertyChanged Target { get; }
        public PropertyInfo TargetProperty { get; }

        public Binding(INotifyPropertyChanged owner, PropertyInfo ownerProperty, INotifyPropertyChanged target, PropertyInfo targetProperty)
        {
            Owner = owner;
            OwnerProperty = ownerProperty;

            Target = target;
            TargetProperty = targetProperty;
        }

        public void Bind()
        {
            Owner.PropertyChanged += OwnerPropertyChanged;
            Target.PropertyChanged += TargetPropertyChanged;
        }

        public void Clear()
        {
            Owner.PropertyChanged -= OwnerPropertyChanged;
            Target.PropertyChanged -= TargetPropertyChanged;
        }

        public void SetOwnerFromTarget()
        {
            Set(Target, TargetProperty, Owner, OwnerProperty);
        }

        private void SetTargetFromOwner()
        {
            Set(Owner, OwnerProperty, Target, TargetProperty);
        }

        private void OwnerPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (!Equals(args.PropertyName, OwnerProperty.Name))
                return;

            SetTargetFromOwner();
        }

        private void TargetPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (!Equals(args.PropertyName, TargetProperty.Name))
                return;

            SetOwnerFromTarget();
        }

        private void Set(object source, PropertyInfo sourceProperty, object destination, PropertyInfo destinationProperty)
        {
            var sourceValue = sourceProperty.GetValue(source);
            var destinationValue = destinationProperty.GetValue(destination);

            if (Equals(sourceValue, destinationValue))
                return;

            destinationProperty.SetValue(destination, sourceValue);
        }
    }

    public abstract class FormModel : BindableBase
    {
        private List<Binding> _bindings = new List<Binding>();

        public virtual void FieldValueChanged()
        {
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
                var field = fieldAttribute.FieldCreator();

                var binding = new Binding(field, fieldAttribute.BindedFieldProperty,
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