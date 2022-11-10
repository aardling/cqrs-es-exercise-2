using DoctorBooker.Infrastructure;

namespace DoctorBooker.Event
{
    public struct SlotWasBooked: IEvent
    {
        public readonly int SlotId;
        public readonly int PatientId;

        public SlotWasBooked(int slotId, int patientId)
        {
            SlotId = slotId;
            PatientId = patientId;
        }

        public int GetStreamId()
        {
            return SlotId;
        }
    }
}
