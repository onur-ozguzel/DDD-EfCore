using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DddAndEfCore.Common
{
    public class Bus : IBus
    {
        public void Send(string message)
        {
            Console.WriteLine($"Message sent: '{message}'");
        }
    }
}
