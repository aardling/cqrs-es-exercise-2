using CommandModel.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandModel
{
    public class Slot
    {
        List<IEvent> _recordedEvents = new List<IEvent>();

        bool _isBooked = false;
        bool _isScheduled = false;
        int _slotId;

        private Slot()
        {
        
        }

        public List<IEvent> GetRecordedEvents()
        {
            return _recordedEvents;
        }

        public static Slot FromHistory(List<IEvent> history)
        {
            var slot = new Slot();

            foreach (var @event in history)
            {
                switch (@event)
                {
                    case SlotWasScheduled slotWasScheduled:
                        slot._isScheduled = true;
                        slot._slotId = slotWasScheduled.SlotId;
                        break;
                }
            }

            return slot;
        }

        public void Book(int patientId)
        {
            if(!_isScheduled)
            {
                throw new Exception("not scheduled");
            }
            
            if(_isBooked)
            {
                //throw
            }

            _recordedEvents.Add(new SlotWasBooked(_slotId, patientId));
        }
    }
}
