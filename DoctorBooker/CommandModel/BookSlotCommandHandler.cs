using DoctorBooker.Event;
using DoctorBooker.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorBooker.CommandModel
{
    public class BookSlotCommandHandler : ICommandHandler<BookSlot>
    {
        private SlotRepository _slotRepository;

        public BookSlotCommandHandler(SlotRepository slotRepository)
        {
            _slotRepository = slotRepository;
        }

        public void Handle(BookSlot command)
        {
            var slot = _slotRepository.FindById(command.SlotId);
            slot.Book(command.PatientId);
            _slotRepository.Save(slot);
        }
    }
}
