
using DoctorBooker.Infrastructure;

namespace DoctorBooker.Event
{
    public struct SlotWasCancelled : IEvent
    {
        public readonly int SlotId;

        public SlotWasCancelled(int slotId)
        {
            SlotId = slotId;
        }

        public int GetStreamId()
        {
            return SlotId;
        }
    }
}
