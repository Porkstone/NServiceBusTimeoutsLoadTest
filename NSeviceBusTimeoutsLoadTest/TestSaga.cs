using System;
using System.Globalization;
using System.Reflection;
using NServiceBus.Saga;
using log4net;

namespace NSeviceBusTimeoutsLoadTest
{
    public class TestSaga : Saga<SagaState>, 
        IAmStartedByMessages<StartSagaMessage>,
        IHandleTimeouts<TestTimeout>,
        IHandleTimeouts<DelayStartTimeout>
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        public void Handle(StartSagaMessage message)
        {
            Data.Uid = message.Uid;
            Logger.DebugFormat("Saga starting for message.Uid={0}, will request timeout for {1}", message.Uid, message.FireTimeoutAt);
            RequestUtcTimeout<TestTimeout>(message.FireTimeoutAt, to => to.State = message.Uid.ToString(CultureInfo.InvariantCulture));
        }

        public void Timeout(TestTimeout timeout)
        {
            Logger.DebugFormat("TestTimeout fired for message.Uid={0}", timeout);
            if (Data.Foo == null)
            {
                Data.Foo = DateTime.UtcNow;
                RequestUtcTimeout<DelayStartTimeout>(DateTime.UtcNow + TimeSpan.FromSeconds(600), delayMessage => delayMessage.State = timeout.State);
            }
        }

        public void Timeout(DelayStartTimeout timeout)
        {
            Logger.DebugFormat("DelayStartTimeout fired for message.Uid={0}", timeout.State);
            MarkAsComplete();
        }
    }
}
