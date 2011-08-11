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
			MenuEntry newGame = new MenuEntry() { Text = "New Game" };
			MenuEntry options = new MenuEntry() { Text = "Options" };
			MenuEntry exit = new MenuEntry() { Text = "Exit" };

			exit.Selected += new EventHandler( exit_Selected );
			options.Selected += new EventHandler( options_Selected );

			MenuEntries.Add( newGame );
			MenuEntries.Add( options );
			MenuEntries.Add( exit );
		}

		void options_Selected( object sender, EventArgs e )
		{
			ScreenManager.AddScreen( new OptionsMenu() );
		}
		void exit_Selected( object sender, EventArgs e )
		{
			ScreenManager.Game.Exit();
		}

		protected override void OnCancel()
		{
			
		}
	}
}
