using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using Microsoft.Xna.Framework;

namespace TDVS.Game.Settings
{
	public class SettingsManager
	{
		static XmlSerializer _xmls;
		static Settings _settings;
		static TDVSGame _Game;

		static SettingsManager()
		{
			_xmls = new XmlSerializer( typeof( Settings ) );
			Load();
		}

		public static void Initialize( TDVSGame game )
		{
			_Game = game;
		}

		public static void Load()
		{
			if ( File.Exists( "Settings.xml" ) )
			{
				using ( FileStream fs = File.OpenRead( "Settings.xml" ) )
				{
					_settings = _xmls.Deserialize( fs ) as Settings;
				}
			}
			else
				_settings = new Settings();
		}
		public static void Save()
		{
			using ( FileStream fs = File.Create( "Settings.xml" ) )
			{
				XmlWriterSettings xws = new XmlWriterSettings()
				{
					Encoding = Encoding.UTF8,
					Indent = true,
					IndentChars = "\t",
					
				};
				using ( XmlWriter xw = XmlWriter.Create( fs, xws ) )
				{
					_xmls.Serialize( xw, _settings );
				}
			}
		}

		public static Settings Settings
		{
			get { return _settings; }
		}

		public static void ApplyVideoSettings()
		{
			HardwareCursor.ApplyCursor( _Game.Window.Handle,
				@"Content\Textures\HWCursor.png",
				Settings.VideoSettings.MouseScale, Point.Zero, new Color( Settings.VideoSettings.MouseColor ) );

			_Game.IsMouseVisible = true;
			_Game.IsFixedTimeStep = false;
			_Game.Graphics.SynchronizeWithVerticalRetrace = Settings.VideoSettings.VSynchEnabled;

			var S = SettingsManager.Settings;
			if ( S.VideoSettings.FullscreenResolution.Width == 0 )
			{
				S.VideoSettings.FullscreenResolution.Width = _Game.GraphicsDevice.DisplayMode.Width;
				S.VideoSettings.FullscreenResolution.Height = _Game.GraphicsDevice.DisplayMode.Height;
			}
			
			_Game.Graphics.IsFullScreen = S.VideoSettings.FullScreen;
			if ( S.VideoSettings.FullScreen )
			{
				_Game.Graphics.PreferredBackBufferWidth = S.VideoSettings.FullscreenResolution.Width;
				_Game.Graphics.PreferredBackBufferHeight = S.VideoSettings.FullscreenResolution.Height;
			}
			else
			{
				_Game.Graphics.PreferredBackBufferWidth = S.VideoSettings.WindowedResolution.Width;
				_Game.Graphics.PreferredBackBufferHeight = S.VideoSettings.WindowedResolution.Height;
			}
			_Game.Graphics.ApplyChanges();
		}
	}
}
