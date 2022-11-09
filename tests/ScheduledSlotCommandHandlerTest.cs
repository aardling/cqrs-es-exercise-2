using CommandModel;
using CommandModel.Command;
using CommandModel.Event;
using NUnit.Framework;
using Store;
using System;
using System.Collections.Generic;

namespace Tests
{
    public class ScheduledSlotCommandHandlerTest : CommandHandlerTest<ScheduleSlot>
    {
        [SetUp]
        public void Setup()
        {
            _store = new EventStore();
            _handler = new ScheduleSlotCommandHandler(_store);
        }

        [Test]
        public void ItShouldAlwaysScheduleASlot()
        {
            var start = DateTime.Now;
            var end = start.AddMinutes(20);
            var doctorId = 1;

            When(new ScheduleSlot(1, start, end));
            Then(new List<IEvent>() { new SlotWasScheduled(1, doctorId, start, end) });
        }        
    }

}