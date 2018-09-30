using System;
using System.ComponentModel;

namespace XForm.EventSubscription
{
    public class NamedPropertyChangedEventSubscription : PropertyChangedEventSubscription
    {
        private readonly string _propertyName;

        public NamedPropertyChangedEventSubscription(INotifyPropertyChanged source,
                                                     string propertyName,
                                                     EventHandler<PropertyChangedEventArgs> targetEventHandler)
            : base(source, targetEventHandler)
        {
            _propertyName = propertyName;
        }

        protected override Delegate CreateEventHandler()
        {
            return new PropertyChangedEventHandler((sender, e) =>
            {
                if (string.IsNullOrEmpty(e.PropertyName) || e.PropertyName == _propertyName)
                {
                    OnSourceEvent(sender, e);
                }
            });
        }
    }
}