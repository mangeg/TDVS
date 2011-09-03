using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using Microsoft.Xna.Framework;

namespace TDVS.Game.Settings
{
	public class SettingsManager
	{
		private static readonly XmlSerializer _sXmls;
		private static TDVSGame _game;

		public static Settings Settings { get; private set; }

		static SettingsManager()
		{
			_sXmls = new XmlSerializer( typeof( Settings ) );
			Load();
		}

		public static void Initialize( TDVSGame game )
		{
			_game = game;
		}

		public static void Load()
		{
			if ( File.Exists( "Settings.xml" ) )
			{
				using ( FileStream fs = File.OpenRead( "Settings.xml" ) )
				{
					Settings = _sXmls.Deserialize( fs ) as Settings;
				}
			}
			else
				Settings = new Settings();
		}
		public static void Save()
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
					_sXmls.Serialize( xw, Settings );
				}
			}
		}

		public static void ApplyVideoSettings()
		{
			HardwareCursor.ApplyCursor( _game.Window.Handle,
				@"Content\Textures\HWCursor.png",
				Settings.VideoSettings.MouseScale, new Point( 9, 3 ), new Color( Settings.VideoSettings.MouseColor ) );

			_game.IsMouseVisible = true;
			_game.IsFixedTimeStep = false;
			_game.Graphics.SynchronizeWithVerticalRetrace = Settings.VideoSettings.VSynchEnabled;

			var s = Settings;
			if ( s.VideoSettings.FullscreenResolution.Width == 0 )
			{
				s.VideoSettings.FullscreenResolution.Width = _game.GraphicsDevice.DisplayMode.Width;
				s.VideoSettings.FullscreenResolution.Height = _game.GraphicsDevice.DisplayMode.Height;
			}
			
			_game.Graphics.IsFullScreen = s.VideoSettings.FullScreen;
			if ( s.VideoSettings.FullScreen )
			{
				_game.Graphics.PreferredBackBufferWidth = s.VideoSettings.FullscreenResolution.Width;
				_game.Graphics.PreferredBackBufferHeight = s.VideoSettings.FullscreenResolution.Height;
			}
			else
			{
				_game.Graphics.PreferredBackBufferWidth = s.VideoSettings.WindowedResolution.Width;
				_game.Graphics.PreferredBackBufferHeight = s.VideoSettings.WindowedResolution.Height;
			}
			_game.Graphics.ApplyChanges();
		}
	}
}
