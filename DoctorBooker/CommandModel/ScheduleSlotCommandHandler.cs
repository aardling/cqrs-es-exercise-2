using DoctorBooker.Event;
using DoctorBooker.Infrastructure;
using System;

namespace DoctorBooker.CommandModel
{
    public class ScheduleSlotCommandHandler : ICommandHandler<ScheduleSlot>
    {

        private SlotRepository _slotRepository;

        public ScheduleSlotCommandHandler(SlotRepository slotRepository)
        {
            _slotRepository = slotRepository;
        }

        public void Handle(ScheduleSlot command)
        {
            var newSlotId = 1; //should be autoincrement, or generated, or ...
            var slot = Slot.Schedule(newSlotId, command.DoctorId, command.StartDateTime, command.EndDateTime);
            _slotRepository.Save(slot); //@todo enforce not making the same slot (id) twice
        }
    }
}
