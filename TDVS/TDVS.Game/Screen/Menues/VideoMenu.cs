using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDVS.Game.Screen;

namespace TDVS.Game.Screen.Menues
{
	public class VideoMenu : MenuScreen
	{
		public VideoMenu()
			: base( "" )
		{
			IsPopup = true;
		}

		protected override void UpdateMenuEntryLocations()
		{			
		}
	}
}
