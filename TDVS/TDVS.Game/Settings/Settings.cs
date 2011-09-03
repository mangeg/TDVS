using Microsoft.Xna.Framework;

namespace TDVS.Game.Settings
{
	public class Settings
	{
		public Settings()
		{
			VideoSettings = new VideoSettings();
		}

		public VideoSettings VideoSettings { get; set; }
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
