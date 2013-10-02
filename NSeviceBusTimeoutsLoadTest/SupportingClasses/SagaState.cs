using System;
using NServiceBus.Saga;

namespace NSeviceBusTimeoutsLoadTest
{
    public class SagaState : ISagaEntity
    {
        [Unique]
        public int Uid { get; set; }

        public DateTime? Foo { get; set; }

        public Guid Id { get; set; }

        public string Originator { get; set; }

        public string OriginalMessageId { get; set; }
    }
}