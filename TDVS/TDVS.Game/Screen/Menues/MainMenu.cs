using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDVS.Game.Screen.Menues
{
	public class MainMenu : MenuScreen
	{
		public MainMenu()
			: base( "Main menu" )
		{
            var newGame = new MenuEntry() { Text = "New Game" };
		    var connect = new MenuEntry {Text = "Connect"};
            var options = new MenuEntry() { Text = "Options" };
            var exit = new MenuEntry() { Text = "Exit" };

		    connect.Selected += new EventHandler(connect_Selected);
            options.Selected += new EventHandler(options_Selected);
            exit.Selected += new EventHandler( exit_Selected );
			

			MenuEntries.Add( newGame );
		    MenuEntries.Add(connect);
			MenuEntries.Add( options );
			MenuEntries.Add( exit );
		}

        void connect_Selected(object sender, EventArgs e)
        {
            ScreenManager.AddScreen(new ConnectionScreen());
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
