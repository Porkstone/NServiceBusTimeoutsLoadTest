

using System.Reflection;

namespace NSeviceBusTimeoutsLoadTest 
{
    using NServiceBus;
    using log4net;    

	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/profiles-for-nservicebus-host
	*/
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantToRunAtStartup
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);         
	    public void Run()
	    {
            // Permit logging config in app.config
            SetLoggingLibrary.Log4Net(log4net.Config.XmlConfigurator.Configure);
            Logger.Debug("NSeviceBusTimeoutsLoadTest endpoint starting...");
	    }

	    public void Stop()
	    {
            Logger.Debug("NSeviceBusTimeoutsLoadTest endpoint is stopping...");
	    }
    }
}