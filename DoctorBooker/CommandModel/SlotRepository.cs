using System;
using DoctorBooker.Infrastructure;

namespace DoctorBooker.CommandModel
{
    public class SlotRepository
    {
        private EventStore _eventStore;

        public SlotRepository(EventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public Slot FindById(int id)
        {
            var events = _eventStore.GetByStreamId(id);
            return Slot.FromHistory(events);
        }

        public void Save(Slot slot)
        {
            _eventStore.AddEvents(slot.GetRecordedEvents());
        }
    }
}