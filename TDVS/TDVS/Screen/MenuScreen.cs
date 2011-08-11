using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TDVS.Screen
{
	public class MenuScreen : GameScreen
	{
		String menuTitle;
		List<MenuEntry> menuEntries = new List<MenuEntry>();
		int selectedEntry = 3;

		private InputAction menuUp;
		private InputAction menuDown;

		public MenuScreen( String title )
		{
			menuTitle = title;

			menuUp = new InputAction( new Keys[] { Keys.Up }, null, true );
			menuDown = new InputAction( new Keys[] { Keys.Down }, null, true );
		}

		protected IList<MenuEntry> MenuEntries
		{
			get { return menuEntries; }
		}

		public override void HandleInput( GameTime gameTime )
		{
			base.HandleInput( gameTime );

			if ( menuUp.Evaluate() )
				selectedEntry--;
			if ( menuDown.Evaluate() )
				selectedEntry++;

			if ( selectedEntry < 0 )
				selectedEntry = menuEntries.Count - 1;
			if ( selectedEntry > menuEntries.Count - 1 )
				selectedEntry = 0;
		}

		protected virtual void OnEnterySelected( int index )
		{
			menuEntries[ index ].OnSelectEntry();
		}

		public override void Update( GameTime gameTime )
		{
			base.Update( gameTime );

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
