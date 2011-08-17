using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDVS.Game.Screen.Menues
{
	public class OptionsMenu : MenuScreen		
	{
		public OptionsMenu()
			: base( "Options" )
		{
			MenuEntry video = new MenuEntry() { Text = "Video Options" };
			MenuEntry sound = new MenuEntry() { Text = "Sound Options" };
			MenuEntry back = new MenuEntry() { Text = "Back" };

			video.Selected += new EventHandler( video_Selected );
			back.Selected += OnCancel;

			MenuEntries.Add( video );
			MenuEntries.Add( sound );
			MenuEntries.Add( back );
		}

		void video_Selected( object sender, EventArgs e )
		{
			ScreenManager.AddScreen( new VideoMenu() );
		}
	}
}
