using DoctorBooker.Event;
using DoctorBooker.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorBooker.CommandModel
{
    public class BookSlotCommandHandler : ICommandHandler<BookSlot>
    {
        private EventStore _store;

        public BookSlotCommandHandler(EventStore store)
        {
            _store = store;
        }

        public void Handle(BookSlot command)
        {

            var events = _store.GetByStreamId(command.SlotId);
            var slot = Slot.FromHistory(events);

            slot.Book(command.PatientId);

            _store.AddEvents(slot.GetRecordedEvents());
        }
    }
}
