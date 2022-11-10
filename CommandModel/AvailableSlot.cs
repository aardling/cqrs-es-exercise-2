using System;

namespace CommandModel
{
    public class AvailableSlot
    {
        public int SlotId { get; set; }
        public int DoctorId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
