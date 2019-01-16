using System.ComponentModel;
using System.Reflection;

namespace XForm.Binding
{
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
}