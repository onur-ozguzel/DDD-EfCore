using DddAndEfCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DddAndEfCore.School
{
    public sealed class StudentEmailChangedEvent : IDomainEvent
    {
        public Guid StudentId { get; set; }
        public Email NewEmail { get; set; }

        public StudentEmailChangedEvent(Guid studentId, Email newEmail)
        {
            StudentId = studentId;
            NewEmail = newEmail;
        }
    }
}
