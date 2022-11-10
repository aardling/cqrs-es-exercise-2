using DoctorBooker.Event;
using DoctorBooker.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorBooker.CommandModel
{
    public class CancelSlotCommandHandler : ICommandHandler<CancelSlot>
    {
        private SlotRepository _slotRepository;

        public CancelSlotCommandHandler(SlotRepository slotRepository)
        {
            _slotRepository = slotRepository;
        }

        public void Handle(CancelSlot command)
        {
            var slot = _slotRepository.FindById(command.SlotId);
            slot.Cancel();
            _slotRepository.Save(slot);
        }
    }
}
