using System;
using NServiceBus;
using NSeviceBusTimeoutsLoadTest;

namespace MessageSender
{
    /// <summary>
    /// This console applicattion will send 5000 StartSagaMessages to the NServiceBusTimeoutsLoadTest endpoint.
    /// </summary>
    public class Program
    {
        public static IBus Bus { get; set; }

        static void Main(string[] args)
        {
            // Insert code here to setup Nservice bus selfhosting 
            Bus = Configure.With().DefaultBuilder()
                .Log4Net()
                .XmlSerializer()
                .MsmqTransport()
                .UnicastBus()
                .SendOnly();

            var p = new Program();
            p.SendMessages();
        }

        public void SendMessages()
        {
            var fireTimeoutAt = DateTime.UtcNow.AddMinutes(15);

            for (int i = 0; i < 5000; i++)
            {
                Bus.Send(new StartSagaMessage() {FireTimeoutAt = fireTimeoutAt, Uid = i});
            }
        }
    }
}
