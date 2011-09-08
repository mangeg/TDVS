using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace TDVS.Common.Logging
{
	public class LogManager
	{
		public static LogFactory Get()
		{
			return Get( String.Empty );
		}

		private static LogFactory Get( string appName )
		{
			if ( !appName.EndsWith( "\\" ) && appName.Length > 1 )
				appName += "\\";
			if ( appName.StartsWith( "\\" ) && appName.Length > 1 )
				appName = appName.Substring( 1 );

			var f = new LogFactory();
			var conf = new XmlLoggingConfiguration( "NLogFormat.xml" );

			var file = conf.FindTargetByName( "mainFile" );
			if ( file != null )
			{
				var fileTarget = file as FileTarget;
				if ( fileTarget != null )
				{
					//fileTarget.FileName = @"${specialfolder:folder=LocalApplicationData:dir=Addett}\" + appName + "${date:format=yy-MM-dd}-${logger}.log";
					//fileTarget.ArchiveFileName = @"${specialfolder:folder=LocalApplicationData:dir=Addett}\" + appName + "${date:format=yy-MM-dd}-${logger}-Archive{##}.log";
				}
			}

			f.Configuration = conf;

			return f;
		}
	}
}
