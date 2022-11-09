using CommandModel.Command;
using CommandModel.Event;
using Store;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandModel
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
            var slot = Slot.fromHistory(events);
            // var slot = slotRepo.findBySlotId(command.slotId);

            slot.book(command.patientId);

            //repo.save(slot)
            _store.AddEvents(slot.getRecordedEvent());
        }
    }
}
