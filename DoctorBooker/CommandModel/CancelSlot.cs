using DoctorBooker.Infrastructure;

namespace DoctorBooker.CommandModel
{
    public struct CancelSlot : ICommand
    {
        public readonly int SlotId;

        public CancelSlot(int slotId)
        {
            SlotId = slotId;
        }
    }
}