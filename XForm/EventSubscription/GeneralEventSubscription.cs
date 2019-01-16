using System;
using System.Reflection;

namespace XForm.EventSubscription
{
    public class GeneralEventSubscription
        : WeakEventSubscription<object, EventArgs>
    {
        public GeneralEventSubscription(object source,
                                        EventInfo eventInfo,
                                        EventHandler<EventArgs> eventHandler)
            : base(source, eventInfo, eventHandler)
        {
        }

        protected override Delegate CreateEventHandler()
        {
            return new EventHandler(OnSourceEvent);
        }
    }
}