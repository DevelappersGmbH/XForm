using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Input;

namespace XForm.EventSubscription
{
    public static class WeakEventSubscriptionExtensions
    {
        public static PropertyChangedEventSubscription WeakSubscribe(this INotifyPropertyChanged source,
                                                                     EventHandler<PropertyChangedEventArgs>
                                                                         eventHandler)
        {
            return new PropertyChangedEventSubscription(source, eventHandler);
        }

        public static CollectionChangedEventSubscription WeakSubscribe(this INotifyCollectionChanged source,
                                                                       EventHandler<NotifyCollectionChangedEventArgs>
                                                                           eventHandler)
        {
            return new CollectionChangedEventSubscription(source, eventHandler);
        }

        public static GeneralEventSubscription WeakSubscribe(this EventInfo eventInfo,
                                                             object source,
                                                             EventHandler<EventArgs> eventHandler)
        {
            return new GeneralEventSubscription(source, eventInfo, eventHandler);
        }

        public static CanExecuteChangedEventSubscription WeakSubscribe(this ICommand source,
                                                                       EventHandler<EventArgs> eventHandler)
        {
            return new CanExecuteChangedEventSubscription(source, eventHandler);
        }
    }
}