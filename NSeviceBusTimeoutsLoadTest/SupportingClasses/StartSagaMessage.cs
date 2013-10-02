using System;
using NServiceBus;

namespace NSeviceBusTimeoutsLoadTest
{
    public class StartSagaMessage : IMessage
    {
        public DateTime FireTimeoutAt { get; set; }
        public int Uid { get; set; }
    }
}