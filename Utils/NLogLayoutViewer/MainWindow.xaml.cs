using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace NLogLayoutViewer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		private readonly MemoryTarget _target;
		private Settings _settings;
		private XmlSerializer _xmls;

		public String FormatText
		{
			get { return _settings.FormatText; }
			set
			{
				_settings.FormatText = value;
				GenerateOutput();
				SaveSettings();
			}
		}
		public String Result
		{
			get { return _settings.Result; }
			set
			{
				_settings.Result = value;
				if ( PropertyChanged != null )
					PropertyChanged( this, new PropertyChangedEventArgs( "Result" ) );
			}
		}

		public MainWindow()
		{
			InitializeComponent();

			_xmls = new XmlSerializer( typeof( Settings ) );
			LoadSettings();

			var config = new LoggingConfiguration();

			_target = new MemoryTarget();
			_target.Name = "memoryLogger";
			_target.Layout = "${message}";
			config.AddTarget( "memoryTarget", _target );

			var rule = new LoggingRule( "*", _target );
			rule.EnableLoggingForLevel( LogLevel.Info );
			rule.EnableLoggingForLevel( LogLevel.Debug );
			rule.EnableLoggingForLevel( LogLevel.Error );
			rule.EnableLoggingForLevel( LogLevel.Fatal );
			rule.EnableLoggingForLevel( LogLevel.Trace );
			rule.EnableLoggingForLevel( LogLevel.Warn );
			config.LoggingRules.Add( rule );

			LogManager.Configuration = config;

			DataContext = this;

			GenerateOutput();
		}

		private void GenerateOutput()
		{
			try
			{
				_target.Layout = _settings.FormatText;//.Replace( Environment.NewLine, "" );
				LogManager.ReconfigExistingLoggers();
				var log = LogManager.GetCurrentClassLogger();

				log.Info( "Some information" );
				log.Error( "Some error" );
				try
				{
					var list = new List<int>();
					list[ 4 ] = 1;
				}
				catch ( Exception ex )
				{
					log.ErrorException( "Forced exception", ex );
				}
				try
				{
					throw new ArgumentException( "Argument exception", "arg1",
												 new ArgumentNullException( "arg2", "Cannot send null params" ) );
				}
				catch ( Exception ex )
				{
					log.ErrorException( "Forced exception2", ex );
				}

				Result = String.Empty;
				foreach ( var t in _target.Logs )
				{
					Result += t;
				}
				_target.Logs.Clear();
			}
			catch ( Exception ex )
			{
				Result = ex.ToString();
			}
			
			
		}

		private void LoadSettings()
		{
			if ( File.Exists( "Settings.xml" ) )
			{
				using ( var fs = File.OpenRead( "Settings.xml" ) )
				{
					_settings = _xmls.Deserialize( fs ) as Settings;
				}
			}
			else
				_settings = new Settings();
		}
		private void SaveSettings()
		{
			using ( var fs = File.Create( "Settings.xml" ) )
			{
				var xws = new XmlWriterSettings
				{
					Encoding = Encoding.UTF8,
					Indent = true,
					IndentChars = "\t"
				};
				using ( var xw = XmlWriter.Create( fs, xws ) )
				{
					_xmls.Serialize( xw, _settings );
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}

	public class Settings
	{
		public String FormatText { get; set; }
		public String Result { get; set; }

		public Settings()
		{
			FormatText = "${message}";
		}
	}
}
