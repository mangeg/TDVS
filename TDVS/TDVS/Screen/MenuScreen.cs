using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TDVS.Screen
{
	public class MenuScreen : GameScreen
	{
		String menuTitle;
		List<MenuEntry> menuEntries = new List<MenuEntry>();
		int selectedEntry = 3;

		public MenuScreen( String title )
		{
			menuTitle = title;
		}

		protected IList<MenuEntry> MenuEntries
		{
			get { return menuEntries; }
		}

		public override void HandleInput( GameTime gameTime )
		{
			if ( false )
			{

			}
		}

		protected virtual void OnEnterySelected( int index )
		{
			menuEntries[ index ].OnSelectEntry();
		}

		public override void Update( GameTime gameTime )
		{
			base.Update( gameTime );

			// Update each nested MenuEntry object.
			for ( int i = 0; i < menuEntries.Count; i++ )
			{
				bool isSelected = ( i == selectedEntry );

				menuEntries[ i ].Update( this, isSelected, gameTime );
			}
		}

		public override void Draw( GameTime gameTime )
		{
			var graphics = ScreenManager.GraphicsDevice;
			var spriteBatch = ScreenManager.SpriteBatch;
			var font = ScreenManager.DefaultFont;

			UpdateMenuEntryLocations();

			var titlePost = new Vector2( graphics.Viewport.Width / 2, 65 );
			var titleOrigin = font.MeasureString( menuTitle ) / 2;
			var titleScale = 1.25f;

			spriteBatch.Begin();
			spriteBatch.DrawString( font, menuTitle, titlePost, Color.Red, 0, titleOrigin, titleScale, SpriteEffects.None, 0 );
			for ( int i = 0; i < menuEntries.Count; i++ )
			{
				bool isSelected = ( i == selectedEntry );
				MenuEntry entry = menuEntries[ i ];
				entry.Draw( this, isSelected, gameTime );
			}
			spriteBatch.End();
		}

		protected virtual void UpdateMenuEntryLocations()
		{
			Vector2 position = new Vector2( 0f, 100f );
			var center = ScreenManager.GraphicsDevice.Viewport.Width / 2;

			for ( int i = 0; i < menuEntries.Count; i++ )
			{
				MenuEntry entry = menuEntries[ i ];
				position.X = center - entry.GetWidth( ScreenManager.DefaultFont ) / 2;

				entry.Position = position;

				position.Y += entry.GetHeight( ScreenManager.DefaultFont );
			}
		}
	}
}
