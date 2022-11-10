using CommandModel.Event;

namespace CommandModel
{
    public interface IEventListener
    {
        void When(IEvent evt);
    }
}

