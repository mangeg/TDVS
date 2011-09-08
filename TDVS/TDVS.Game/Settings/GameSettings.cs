using Microsoft.Xna.Framework;
using TDVS.Common.Settings;

namespace TDVS.Game.Settings
{
	public class GameSettings : SettingsBase 
	{
		public GameSettings()
		{
			VideoSettings = new VideoSettings();
		}

		public VideoSettings VideoSettings { get; set; }

		public void ApplyVideoSettings( Microsoft.Xna.Framework.Game game )
		{
			HardwareCursor.ApplyCursor( game.Window.Handle,
				@"Content\Textures\HWCursor.png",
				VideoSettings.MouseScale, new Point( 9, 3 ), new Color( VideoSettings.MouseColor ) );

			game.IsMouseVisible = true;
			game.IsFixedTimeStep = false;

			var devManager = (GraphicsDeviceManager) game.Services.GetService( typeof (IGraphicsDeviceManager) );

			devManager.SynchronizeWithVerticalRetrace = VideoSettings.VSynchEnabled;

			if ( VideoSettings.FullscreenResolution.Width == 0 )
			{
				VideoSettings.FullscreenResolution.Width = game.GraphicsDevice.DisplayMode.Width;
				VideoSettings.FullscreenResolution.Height = game.GraphicsDevice.DisplayMode.Height;
			}

			devManager.IsFullScreen = VideoSettings.FullScreen;
			if ( VideoSettings.FullScreen )
			{
				devManager.PreferredBackBufferWidth = VideoSettings.FullscreenResolution.Width;
				devManager.PreferredBackBufferHeight = VideoSettings.FullscreenResolution.Height;
			}
			else
			{
				devManager.PreferredBackBufferWidth = VideoSettings.WindowedResolution.Width;
				devManager.PreferredBackBufferHeight = VideoSettings.WindowedResolution.Height;
			}
			devManager.ApplyChanges();
		}
	}

	public class VideoSettings
	{
		public VideoSettings()
		{
			MouseScale = 0.9f;
			MouseColor = new Vector4(0.99f, 0.64f, 0.8f, 1f);
			FullScreen = false;
			VSynchEnabled = true;
			WindowedResolution = new Resolution
			{
				Width = 800,
				Height = 600,
			};
			FullscreenResolution = new Resolution();
		}

		public float MouseScale { get; set; }
		public Vector4 MouseColor { get; set; }
		public bool FullScreen { get; set; }
		public bool VSynchEnabled { get; set; }

		public Resolution WindowedResolution { get; set; }
		public Resolution FullscreenResolution { get; set; }
	}

	public class Resolution
	{
		public int Width { get; set; }
		public int Height { get; set; }
	}
}
