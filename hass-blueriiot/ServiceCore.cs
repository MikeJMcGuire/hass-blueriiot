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
		private static string _strServiceName = "SARAHPool";
		private static string _strServiceDescription = "SARAH Pool Controller";
		private static string _strHAKey = "";
		private static IHost _webHost;

        public static void Start()
        {
			string strUser, strPassword;

			Logging.WriteLog("ServiceCore.Start() Built: {0}", Properties.Resources.BuildDate);

			if (!Configuration.GetConfiguration("BlueRiiotUser", out strUser))
				return;
			
			if (!Configuration.GetPrivateConfiguration("BlueRiiotPassword", out strPassword))
				return;

			if (!Configuration.GetPrivateConfiguration("SUPERVISOR_TOKEN", out _strHAKey))
				return;

			BlueRiiot.Start(strUser, strPassword);
					
			try
			{
				_webHost = Host.CreateDefaultBuilder().ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<ASPNETCoreStartup>();
				}).Build();
			}
			catch (Exception eException)
			{
				Logging.WriteLogError("ServiceCore.Start()", eException, "Unable to build Kestrel instance.");
				return;
			}

			_webHost.Run();

			Logging.WriteLog("ServiceCore.Start() Started");
		}

		public static void HARegister()
		{
			Logging.WriteLog("ServiceCore.HARegister()");

			/*MQTT.SendMessage("homeassistant/sensor/sarah/sensor_pool_temperature/config",
				"{{\"name\":\"Pool Temperature\",\"unique_id\":\"{1}-0\",\"device\":{{\"identifiers\":[\"{1}\"],\"name\":\"{2}\",\"model\":\"Container\",\"manufacturer\":\"HMX\"}},\"state_topic\":\"sarah/sensor_pool/temperature\",\"unit_of_measurement\":\"°C\",\"availability_topic\":\"{0}/status\"}}", _strServiceName.ToLower(), _strServiceName, _strServiceDescription);

			MQTT.SendMessage("homeassistant/sensor/sarah/sensor_pool_ph/config",
				"{{\"name\":\"Pool pH\",\"unique_id\":\"{1}-1\",\"device\":{{\"identifiers\":[\"{1}\"],\"name\":\"{2}\",\"model\":\"Container\",\"manufacturer\":\"HMX\"}},\"state_topic\":\"sarah/sensor_pool/ph\",\"availability_topic\":\"{0}/status\"}}", _strServiceName.ToLower(), _strServiceName, _strServiceDescription);

			MQTT.SendMessage("homeassistant/sensor/sarah/sensor_pool_orp/config",
				 "{{\"name\":\"Pool Orp\",\"unique_id\":\"{1}-2\",\"device\":{{\"identifiers\":[\"{1}\"],\"name\":\"{2}\",\"model\":\"Container\",\"manufacturer\":\"HMX\"}},\"state_topic\":\"sarah/sensor_pool/orp\",\"unit_of_measurement\":\"mV\",\"availability_topic\":\"{0}/status\"}}", _strServiceName.ToLower(), _strServiceName, _strServiceDescription);

			MQTT.SendMessage("homeassistant/sensor/sarah/sensor_pool_salinity/config",
				"{{\"name\":\"Pool Salinity\",\"unique_id\":\"{1}-3\",\"device\":{{\"identifiers\":[\"{1}\"],\"name\":\"{2}\",\"model\":\"Container\",\"manufacturer\":\"HMX\"}},\"state_topic\":\"sarah/sensor_pool/salinity\",\"unit_of_measurement\":\"ppm\",\"availability_topic\":\"{0}/status\"}}", _strServiceName.ToLower(), _strServiceName, _strServiceDescription);*/
		}

		public static void Stop()
        {
            Logging.WriteLog("ServiceCore.Stop()");

			

			Logging.WriteLog("ServiceCore.Stop() Stopped");
		}
	}
}
