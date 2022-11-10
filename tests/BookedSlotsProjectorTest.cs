using DoctorBooker.Event;
using DoctorBooker.Infrastructure;
using DoctorBooker.QueryModel;
using NUnit.Framework;
using System;

namespace Tests
{

    public class BookedSlotsProjectorTest
    {
        protected BookedSlotsProjector _projector;
        protected EventStore _store;

        [SetUp]
        public void Setup()
        {
            _store = new EventStore();
            _projector = new BookedSlotsProjector();
            _store.Subscribe(_projector);
        }

        [Test]
        public void ItShouldReturnNoSlotsWhenNothingWasBooked()
        {
            var patientId = 1;
            var slots = _projector.GetAllBookedSlotsForPatient(patientId);
            Assert.AreEqual(0, slots.Count);
        }

        [Test]
        public void ItShouldReturnBookedSlotsWhenSlotWasBooked()
        {
            var slotId = 1;
            var doctorId = 1;
            var start = DateTime.Now;
            var end = start.AddMinutes(20);
            var patientId = 1;

            _store.AddEvent(new SlotWasScheduled(slotId, doctorId, start, end) { });
            _store.AddEvent(new SlotWasBooked(slotId, patientId) { });


            var slots = _projector.GetAllBookedSlotsForPatient(patientId);
            Assert.AreEqual(1, slots.Count);
        }

        [Test]
        public void ItShouldReturnNoSlotsWhenBookedSlotWasCancelled()
        {
            var slotId = 1;
            var doctorId = 1;
            var start = DateTime.Now;
            var end = start.AddMinutes(20);
            var patientId = 1;

            _store.AddEvent(new SlotWasScheduled(slotId, doctorId, start, end) { });
            _store.AddEvent(new SlotWasBooked(slotId, patientId) { });
            _store.AddEvent(new SlotWasCancelled(slotId) { });


            var slots = _projector.GetAllBookedSlotsForPatient(patientId);
            Assert.AreEqual(0, slots.Count);
        }

        // TODO Add more scenarios
    }
}