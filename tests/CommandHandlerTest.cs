using DoctorBooker.Infrastructure;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests
{
    public class CommandHandlerTest<T> where T: ICommand
    {
        protected ICommandHandler<T> _handler;
        protected EventStore _store;
        private T _command;

        public CommandHandlerTest()
        {
            
        }

        protected void When(T command)
        {
            _command = command;
        }

        protected void Then(List<IEvent> expectedEvents)
        {
            _handler.Handle(_command);

            var actualEvents = _store.GetNewEvents();

            Assert.AreEqual(expectedEvents, actualEvents);
            Assert.IsTrue(actualEvents.SequenceEqual(expectedEvents));
        }

        protected void Then<E>() where E: Exception
        {            
            Assert.Throws<E>(() => _handler.Handle(_command));
            var newEvents = _store.GetNewEvents();
            Assert.That(newEvents.Count == 0);
        }

                    


        protected void Given(IEvent @event)
        {
            _store.AddHistoricalEvent(@event);
        }
    }
}
