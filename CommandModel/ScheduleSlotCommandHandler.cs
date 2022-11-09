using CommandModel.Command;
using CommandModel.Event;
using Store;
using System;

namespace CommandModel
{
    public class ScheduleSlotCommandHandler : ICommandHandler<ScheduleSlot>
    {

        private EventStore _store;

        public ScheduleSlotCommandHandler(EventStore store)
        {
            _store = store;
        }

        public void Handle(ScheduleSlot command)
        {
            //1. identify stream/aggregate/consistency boundary
            //2. fetch history
            //3. build up state from history 
            //4. enforce constraints -> or throw exceptions

            //5. create event(s)
            var newEvent = new SlotWasScheduled(1, command.DoctorId, command.StartDateTime, command.EndDateTime);

            //6. append events to history
            _store.AddEvent(newEvent);
        }
    }
}
