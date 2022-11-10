using DoctorBooker.Event;
using DoctorBooker.Infrastructure;
using DoctorBooker.QueryModel;
using NUnit.Framework;
using System;

namespace Tests
{

    public class AvailableSlotsProjectorTests
    {
        protected AvailableSlotsProjector _projector;
        protected EventStore _store;

        [SetUp]
        public void Setup()
        {
            _store = new EventStore();
            _projector = new AvailableSlotsProjector();
            _store.Subscribe(_projector);
        }

        [Test]
        public void ItShouldReturnNoAvailableSlots()
        {
            var slots = _projector.GetAllAvailableSlotsForDay(DateTime.Now);
            Assert.AreEqual(0, slots.Count);
        }

        [Test]
        public void ItShouldReturnAvailableSlots()
        {
            var slotId = 1;
            var doctorId = 1;
            var start = DateTime.Now;
            var end = start.AddMinutes(20);

            _store.AddEvent(new SlotWasScheduled(slotId, doctorId, start, end) {});

            var slots = _projector.GetAllAvailableSlotsForDay(start);
            Assert.AreEqual(1, slots.Count);
        }

        [Test]
        public void ItShouldNotReturnBookedSlots()
        {
            var slotId = 1;
            var patientId = 1;
            var doctorId = 1;
            var start = DateTime.Now;
            var end = start.AddMinutes(20);

            _store.AddEvent(new SlotWasScheduled(slotId, doctorId, start, end));
            _store.AddEvent(new SlotWasBooked(slotId, patientId));

            var slots = _projector.GetAllAvailableSlotsForDay(start);
            Assert.AreEqual(0, slots.Count);
        }

        [Test]
        public void ItShouldMakeCancelledBookingSlotsAvailableAgain()
        {
            var slotId = 1;
            var patientId = 1;
            var doctorId = 1;
            var start = DateTime.Now;
            var end = start.AddMinutes(20);

            _store.AddEvent(new SlotWasScheduled(slotId, doctorId, start, end));
            _store.AddEvent(new SlotWasBooked(slotId, patientId));
            _store.AddEvent(new SlotWasCancelled(slotId));

            var slots = _projector.GetAllAvailableSlotsForDay(start);
            Assert.AreEqual(1, slots.Count);
        }
    }
}