using CommandModel.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Store
{
    public class EventStore
    {
        private List<IEvent> _historicalEvents = new List<IEvent>();
        private List<IEvent> _newEvents = new List<IEvent>();

        public void AddEvent(IEvent newEvent)
        {
            _newEvents.Add(newEvent);            
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
    }
}
