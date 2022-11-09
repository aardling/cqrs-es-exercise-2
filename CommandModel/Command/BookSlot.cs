using System;

namespace CommandModel.Command
{
    public struct BookSlot: ICommand
    {
        public readonly int PatientId;
        public readonly int SlotId;

        public BookSlot(int slotId, int patientId)
        {
            PatientId = patientId;
            SlotId = slotId;
        }
    }
}