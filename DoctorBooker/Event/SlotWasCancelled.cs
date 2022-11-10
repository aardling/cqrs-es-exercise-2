
using DoctorBooker.Infrastructure;

namespace DoctorBooker.Event
{
    public struct SlotWasCancelled : IEvent
    {
        public readonly int SlotId;
        public readonly int PatientId;

        public SlotWasCancelled(int slotId, int patientId)
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
