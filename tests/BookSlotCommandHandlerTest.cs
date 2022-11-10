﻿using CommandModel;
using CommandModel.Command;
using CommandModel.Event;
using CommandModel.Exceptions;
using NUnit.Framework;
using Store;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{

    public class BookSlotCommandHandlerTest: CommandHandlerTest<BookSlot>
    {
        [SetUp]
        public void Setup()
        {
            _store = new EventStore();
            _handler = new BookSlotCommandHandler(_store);
        }

        [Test]
        public void ItShouldBookASlot()
        {
            var slotId = 1;
            var patientId = 1;
            var doctorId = 1;
            var start = DateTime.Now;
            var end = start.AddMinutes(20);

            Given(new SlotWasScheduled(slotId, doctorId, start, end));
            When(new BookSlot(slotId, patientId));
            Then(new List<IEvent>() { new SlotWasBooked(slotId, patientId) });
        }

        [Test]
        public void ItShouldNotBookANonExistingSlot()
        {
            var slotId = 1;
            var patientId = 1;

            When(new BookSlot(slotId, patientId));
            Then<SlotNotScheduledException>();
        }

        [Test]
        public void ItShouldNotBookAnAlreadyBookedSlot()
        {
            var slotId = 1;
            var patientId = 1;
            var doctorId = 1;
            var start = DateTime.Now;
            var end = start.AddMinutes(20);

            var otherPatientId = 2;

            Given(new SlotWasScheduled(slotId, doctorId, start, end));
            Given(new SlotWasBooked(slotId, patientId));
            When(new BookSlot(slotId, otherPatientId));
            Then<Exception>();
        }
    }
}
