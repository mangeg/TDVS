using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TDVS.Settings
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
			MouseScale = 1f;
			MouseColor = Color.Red.ToVector4();
			FullScreen = false;
			WindowedResolution = new Resolution()
			{
				Width = 800,
				Height = 600,
			};
			FullscreenResolution = new Resolution();
		}

		public float MouseScale { get; set; }
		public Vector4 MouseColor { get; set; }
		public bool FullScreen { get; set; }

		public Resolution WindowedResolution { get; set; }
		public Resolution FullscreenResolution { get; set; }
	}

	public class Resolution
	{
		public int Width { get; set; }
		public int Height { get; set; }
	}
}
