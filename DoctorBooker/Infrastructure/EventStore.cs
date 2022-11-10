using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoctorBooker.Infrastructure
{
    public class EventStore
    {
        private List<IEvent> _historicalEvents = new List<IEvent>();
        private List<IEvent> _newEvents = new List<IEvent>();

        private List<IEventListener> _subscriptions = new List<IEventListener>();


        public void AddEvent(IEvent newEvent)
        {
            _newEvents.Add(newEvent);

            foreach (var subscriber in _subscriptions)
            {
                subscriber.When(newEvent);
            }
        }

        public void AddEvents(List<IEvent> events)
        {
            foreach (var @event in events)
            {
                AddEvent(@event);
            }
        }

        public List<IEvent> GetByStreamId(int streamId)
        {
            return _historicalEvents.Concat(_newEvents).Where(x => x.GetStreamId() == streamId).ToList();
        }

        public List<IEvent> GetNewEvents()
        {
            return _newEvents;
        }

        public void AddHistoricalEvent(IEvent @event)
        {
            _historicalEvents.Add(@event);
        }

        public void Subscribe(IEventListener eventListener)
        {
            _subscriptions.Add(eventListener);
        }
    }
}
