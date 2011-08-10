using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDVS.Screen.Menues
{
	public class MainMenu : MenuScreen
	{
		public MainMenu()
			: base( "Main menu" )
		{
			MenuEntries.Add( new MenuEntry() { Text = "Myuu" } );
			MenuEntries.Add( new MenuEntry() { Text = "Myuu1" } );
			MenuEntries.Add( new MenuEntry() { Text = "Myuu2sgdgsdg" } );
			MenuEntries.Add( new MenuEntry() { Text = "Myuu3sdsdh" } );
			MenuEntries.Add( new MenuEntry() { Text = "Myuu4sdgsdg" } );
			MenuEntries.Add( new MenuEntry() { Text = "Myuu52523" } );
		}
	}
}
