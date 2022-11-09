namespace CommandModel.Event
{
    public class SlotWasBooked
    {
        public readonly int SlotId;
        public readonly int PatientId;

        public SlotWasBooked(int slotId, int patientId)
        {
            SlotId = slotId;
            PatientId = patientId;
        }
    }
}
