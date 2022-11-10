using DoctorBooker.CommandModel;
using DoctorBooker.Event;
using DoctorBooker.Infrastructure;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Tests
{
    public class CancelSlotCommandHandlerTest : CommandHandlerTest<CancelSlot>
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

            When(new CancelSlot(slotId, patientId));
            Then(new List<IEvent>() { new SlotWasCancelled(slotId, patientId) });
        }

        [Test]
        public void ItShouldThrowWhenTryingToCancelSomeoneElsesSlot()
        {
            var slotId = 1;
            var patientId = 1;
            var doctorId = 1;
            var start = DateTime.Now;
            var end = start.AddMinutes(20);

            Given(new SlotWasScheduled(slotId, doctorId, start, end));
            Given(new SlotWasBooked(slotId, patientId));

            When(new CancelSlot(slotId, 2));
            Then<Exception>();
        }
    }

}