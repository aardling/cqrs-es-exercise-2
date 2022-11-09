using System;

namespace CommandModel.Command
{
    public class BookSlot
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