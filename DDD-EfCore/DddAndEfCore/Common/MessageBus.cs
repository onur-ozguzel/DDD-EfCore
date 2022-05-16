using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DddAndEfCore.Common
{
    public class MessageBus
    {
        private readonly IBus _bus;

        public MessageBus(IBus bus)
        {
            _bus = bus;
        }

        public void SendEmailChangedMessage(Guid studentId, string newEmail)
        {
            _bus.Send("Type: STUDENT_EMAIL_CHANGED; " +
                $"Id: {studentId};" +
                $"NewEmail: {newEmail}");
        }
    }
}
