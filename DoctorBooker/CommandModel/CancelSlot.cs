using DoctorBooker.Infrastructure;

namespace DoctorBooker.CommandModel
{
    public struct CancelSlot : ICommand
    {
        public readonly int SlotId;
        public readonly int PatientId;

        public CancelSlot(int slotId, int patientId)
        {
            SlotId = slotId;
            PatientId = patientId;
        }
    }
}