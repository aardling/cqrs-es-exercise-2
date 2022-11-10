namespace DoctorBooker.Infrastructure
{
    public interface IEvent
    {
        int GetStreamId();
    }
}