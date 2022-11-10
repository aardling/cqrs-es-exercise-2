using DoctorBooker.Event;
using DoctorBooker.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace DoctorBooker.QueryModel
{
    public class BookedSlotsProjector : IEventListener
    {
        private List<AvailableSlot> _bookedSlots;

        public BookedSlotsProjector()
        {
            _bookedSlots = new List<AvailableSlot>();
        }

        public void When(IEvent evt)
        {
            if (evt is SlotWasBooked bookedEvent)
            {
                When(bookedEvent);
            }
            if (evt is SlotWasCancelled canceledEvent)
            {
                When(canceledEvent);
            }
        }

        public void When(SlotWasBooked evt)
        {
            _bookedSlots.Add(new AvailableSlot
            {
                SlotId = evt.SlotId,
                PatientId = evt.PatientId
            });
        }

        public void When(SlotWasCancelled evt)
        {
            var slot = _bookedSlots.SingleOrDefault(x => x.SlotId == evt.SlotId);
            if (slot != null)
            {
                _bookedSlots.Remove(slot);
            }
        }

        public List<AvailableSlot> GetAllBookedSlotsForPatient(int patientId)
        {
            return _bookedSlots.Where(slot => slot.PatientId == patientId).ToList();
        }
    }
}