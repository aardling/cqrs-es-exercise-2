using DoctorBooker.Event;
using System;
using System.Collections.Generic;
using DoctorBooker.Infrastructure;

namespace DoctorBooker.CommandModel
{
    public class Slot
    {
        List<IEvent> _recordedEvents;

        bool _isBooked = false;
        bool _isScheduled = false;
        int _slotId;
        int? _patientId;

        private Slot()
        {
            _recordedEvents = new List<IEvent>();
        }

        public List<IEvent> GetRecordedEvents()
        {
            return _recordedEvents;
        }

        private void When(SlotWasScheduled slotWasScheduled)
        {
            _isScheduled = true;
            _slotId = slotWasScheduled.SlotId;
        }

        private void When(SlotWasBooked slotWasBooked)
        {
            _isBooked = true;
            _patientId = slotWasBooked.PatientId;
        }

        private void When(SlotWasCancelled slotWasCancelled)
        {
            _isBooked = false;
            _patientId = null;
        }

        private void When(IEvent @event)
        {
            switch (@event)
            {
                case SlotWasScheduled slotWasScheduled:
                    When(slotWasScheduled);
                    break;
                case SlotWasBooked slotWasBooked:
                    When(slotWasBooked);
                    break;
                case SlotWasCancelled slotWasCancelled:
                    When(slotWasCancelled);
                    break;
            }
        }

        public static Slot FromHistory(List<IEvent> history)
        {
            var slot = new Slot();

            foreach (var @event in history)
            {
                slot.When(@event);
            }

            return slot;
        }

        public static Slot Schedule(int slotId, int doctorId, DateTime startTime, DateTime endTime)
        {
            var slot = new Slot();
            slot.RecordThat(new SlotWasScheduled(slotId, doctorId, startTime, endTime));
            return slot;
        }

        public void Book(int patientId)
        {
            if (!_isScheduled)
            {
                throw new SlotNotScheduledException();
            }

            if (_isBooked)
            {
                throw new Exception("already booked");
            }

            RecordThat(new SlotWasBooked(_slotId, patientId));
        }

        public void Cancel(int patientId)
        {
            if (!_isScheduled)
            {
                throw new SlotNotScheduledException();
            }

            if (!_isBooked)
            {
                throw new Exception("not booked");
            }

            if(_patientId != patientId)
            {
                throw new Exception("Cannot cancel someone else's slot");
            }

            RecordThat(new SlotWasCancelled(_slotId, patientId));
        }

        private void RecordThat(IEvent newEvent)
        {
            _recordedEvents.Add(newEvent);
            When(newEvent);
        }
    }
}
