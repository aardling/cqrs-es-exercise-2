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

        public Slot FromHistory(List<IEvent> history)
        {
            //rebuild Slot from history
            // slotId = 
            // scheduled = true
            // booked = true

            return null;
        }

        public void Book(int patientId)
        {
            if(_slotId == 0) {
                //throw
            }
            
            if(_isBooked) {
                //throw
            }

            _recordedEvents.Add(new SlotWasBooked(_slotId, patientId))
        }
    }
}
