using DddAndEfCore.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DddAndEfCore.Common
{
    public class EventDispatcher
    {
        private readonly MessageBus _messageBus;
        public EventDispatcher(MessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public void Dispatch(IEnumerable<IDomainEvent> events)
        {
            foreach (IDomainEvent ev in events)
            {
                Dispatch(ev);
            }
        }

        private void Dispatch(IDomainEvent ev)
        {
            switch (ev)
            {
                case StudentEmailChangedEvent emailChangedEvent:
                    _messageBus.SendEmailChangedMessage(
                        emailChangedEvent.StudentId,
                        emailChangedEvent.NewEmail);
                    break;

                default:
                    throw new Exception($"Unknown event type: '{ev.GetType()}'");
            }
        }
    }
}
