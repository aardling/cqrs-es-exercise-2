using System;

namespace CommandModel.Event
{
    public class SlotWasScheduled
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
    }
}
