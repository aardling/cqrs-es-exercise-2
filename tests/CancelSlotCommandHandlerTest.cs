using DoctorBooker;
using DoctorBooker.CommandModel;
using DoctorBooker.Event;
using DoctorBooker.Infrastructure;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{

    public class CancelSlotCommandHandlerTest: CommandHandlerTest<CancelSlot>
    {
        [SetUp]
        public void Setup()
        {
            _store = new EventStore();
            _handler = new CancelSlotCommandHandler(new SlotRepository(_store));
        }

        [Test]
        public void ItShouldCancelABookedSlot()
        {
            var slotId = 1;
            var patientId = 1;
            var doctorId = 1;
            var start = DateTime.Now;
            var end = start.AddMinutes(20);

            Given(new SlotWasScheduled(slotId, doctorId, start, end));
            Given(new SlotWasBooked(slotId, patientId));
            When(new CancelSlot(slotId));
            Then(new List<IEvent>() { new SlotWasCancelled(slotId) });
        }

        [Test]
        public void ItShouldNotCancelANonExistingSlot()
        {
            var slotId = 1;

            When(new CancelSlot(slotId));
            Then<SlotNotScheduledException>();
        }

        [Test]
        public void ItShouldNotCancelANonBookedSlot()
        {
            var slotId = 1;
            var doctorId = 1;
            var start = DateTime.Now;
            var end = start.AddMinutes(20);

            Given(new SlotWasScheduled(slotId, doctorId, start, end));
            When(new CancelSlot(slotId));
            Then<Exception>();
        }
    }
}
