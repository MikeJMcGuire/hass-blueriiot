using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;

namespace HMX.HASSBlueriiot
{
    public class ServiceCore
    {
		private static string _strHAKey = "";

        public static void Start()
        {
			IHost webHost;
			string strUser, strPassword;

			Logging.WriteLog("ServiceCore.Start() Built: {0}", Properties.Resources.BuildDate);

			if (!Configuration.GetConfiguration("BlueRiiotUser", out strUser))
				return;
			
			if (!Configuration.GetPrivateConfiguration("BlueRiiotPassword", out strPassword))
				return;

			if (!Configuration.GetPrivateConfiguration("SUPERVISOR_TOKEN", out _strHAKey))
				return;

			HomeAssistant.Initialise(_strHAKey); 
			BlueRiiot.Start(strUser, strPassword);			
					
			try
			{
				webHost = Host.CreateDefaultBuilder().ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<ASPNETCoreStartup>();
				}).Build();
			}
			catch (Exception eException)
			{
				Logging.WriteLogError("ServiceCore.Start()", eException, "Unable to build Kestrel instance.");
				return;
			}

			webHost.Run();

			Logging.WriteLog("ServiceCore.Start() Started");
		}

		public static void Stop()
        {
            Logging.WriteLog("ServiceCore.Stop()");		

			Logging.WriteLog("ServiceCore.Stop() Stopped");
		}
	}
}
