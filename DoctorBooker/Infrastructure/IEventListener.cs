namespace DoctorBooker.Infrastructure
{
    public interface IEventListener
    {
        void When(IEvent evt);
    }
}

