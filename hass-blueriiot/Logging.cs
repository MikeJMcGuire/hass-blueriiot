﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HMX.HASSBlueriiot
{
    public class Logging
    {
		public static void WriteLog(string strFormat, params object[] strParams)
		{
			WriteLogEntry(strFormat, strParams);
		}

		public static void WriteLogError(string strFunction, long lRequestId, string strFormat, params object[] strParams)
		{
			WriteLogEntry(string.Format("{0} [0x{1}] Error: {2}", strFunction, lRequestId.ToString("X8"), string.Format(strFormat, strParams)));
		}

		public static void WriteLogError(string strFunction, string strFormat, params object[] strParams)
		{
			WriteLogEntry(string.Format("{0} Error: {1}", strFunction, string.Format(strFormat, strParams)));
		}

		public static void WriteLogError(string strFunction, Exception eException, string strFormat, params object[] strParams)
		{
			WriteLogEntry(string.Format("{0} Error ({1}): ", strFunction, eException.GetType().ToString()) + strFormat + " " + eException.Message, strParams);
		}
		
		public static void WriteLogError(string strFunction, long lRequestId, Exception eException, string strFormat, params object[] strParams)
		{
			WriteLogEntry(string.Format("{0} [0x{1}] Error ({2}): ", strFunction, lRequestId.ToString("X8"), eException.GetType().ToString()) + strFormat + " " + eException.Message, strParams);
		}

		private static void WriteLogEntry(string strFormat, params object[] strParams)
		{
			DateTime dtNow = DateTime.Now;
			string strLogData, strLogMessage;

			if (strParams.Length == 0)
				strLogMessage = strFormat;
			else
				strLogMessage = string.Format(strFormat, strParams);

			strLogData = dtNow.ToString("dd-MM-yyyy HH:mm:ss.ff ") + strLogMessage;

			if (strLogData.EndsWith(Environment.NewLine))
				strLogData = strLogData.Substring(0, strLogData.Length - Environment.NewLine.Length);
			strLogData.Replace(Environment.NewLine, " ");

			Console.WriteLine(strLogData);
		}
    }
}
