using System;

namespace TDVS.Common.Settings
{
	[Serializable]
	public class GameSettingsBase : SettingsBase
	{
		public GameSettingsBase()
		{
			VideoSettings = new VideoSettings();
		}

		public VideoSettings VideoSettings { get; set; }
	}
}