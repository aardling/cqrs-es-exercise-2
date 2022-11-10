using CommandModel.Event;
using System;
using System.Collections.Generic;
using System.Text;
using CommandModel.Exceptions;

namespace CommandModel
{
    public class Slot
    {
        List<IEvent> _recordedEvents;

        bool _isBooked = false;
        bool _isScheduled = false;
        int _slotId;

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

        public void Book(int patientId)
        {
            if(!_isScheduled)
            {
                throw new SlotNotScheduledException();
            }
            
            if(_isBooked)
            {
                throw new Exception("already booked");
            }

            RecordThat(new SlotWasBooked(_slotId, patientId));   
        }

        public void Cancel()
        {
            if (!_isScheduled)
            {
                throw new SlotNotScheduledException();
            }

            if (! _isBooked)
            {
                throw new Exception("not booked");
            }

            RecordThat(new SlotWasCancelled(_slotId));
        }

        private void RecordThat(IEvent newEvent)
        {
            _recordedEvents.Add(newEvent);
            When(newEvent);
        }
    }
}
