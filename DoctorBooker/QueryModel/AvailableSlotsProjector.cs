using DoctorBooker.Event;
using DoctorBooker.Infrastructure;
using DoctorBooker.QueryModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DoctorBooker.QueryModel
{
    public class AvailableSlotsProjector : IEventListener
    {
        private List<AvailableSlot> _availableSlots;
        private List<AvailableSlot> _bookedSlots;

        public AvailableSlotsProjector()
        {
            _availableSlots = new List<AvailableSlot>();
            _bookedSlots = new List<AvailableSlot>();
        }

        public void When(IEvent evt)
        {
            if (evt is SlotWasScheduled scheduledEvent)
            {
                When(scheduledEvent);
            }
            if (evt is SlotWasBooked bookedEvent)
            {
                When(bookedEvent);
            }
            if (evt is SlotWasCancelled canceledEvent)
            {
                When(canceledEvent);
            }
        }

        public void When(SlotWasScheduled evt)
        {
            _availableSlots.Add(new AvailableSlot
            {
                SlotId = evt.SlotId,
                DoctorId = evt.DoctorId,
                StartTime = evt.StartDateTime,
                EndTime = evt.EndDateTime
            });
        }

        public void When(SlotWasBooked evt)
        {
            var slot = _availableSlots.SingleOrDefault(x => x.SlotId == evt.SlotId);
            if (slot != null)
            {
                _availableSlots.Remove(slot);
                _bookedSlots.Add(slot);
            }
        }

        public void When(SlotWasCancelled evt)
        {
            var slot = _bookedSlots.SingleOrDefault(x => x.SlotId == evt.SlotId);
            if (slot != null)
            {
                _bookedSlots.Remove(slot);
                _availableSlots.Add(slot);
            }
        }

        public List<AvailableSlot> GetAllAvailableSlotsForDay(DateTime day)
        {
            return _availableSlots.Where(s => s.StartTime.Date == day.Date).ToList();
        }
    }
}