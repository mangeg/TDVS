using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDVS.Screen.Menues
{
	public class OptionsMenu : MenuScreen		
	{
		public OptionsMenu()
			: base( "Options" )
		{
			MenuEntry video = new MenuEntry() { Text = "Video Options" };
			MenuEntry sound = new MenuEntry() { Text = "Sound Options" };
			MenuEntry back = new MenuEntry() { Text = "Back" };

			back.Selected += OnCancel;

			MenuEntries.Add( video );
			MenuEntries.Add( sound );
			MenuEntries.Add( back );
		}
	}
}
