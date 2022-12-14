using System;
using DoctorBooker.Infrastructure;

namespace DoctorBooker.Event
{
    public struct SlotWasScheduled: IEvent
    {
        public readonly DateTime StartDateTime;
        public readonly DateTime EndDateTime;
        public readonly int DoctorId;
        public readonly int SlotId;

        public SlotWasScheduled(int slotId, int doctorId, DateTime start, DateTime end)
        {
            SlotId = slotId;
            DoctorId = doctorId;
            StartDateTime = start;
            EndDateTime = end;
        }

        public int GetStreamId()
        {
            return SlotId;
        }
    }
}
