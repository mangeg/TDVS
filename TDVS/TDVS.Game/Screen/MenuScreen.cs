using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TDVS.Game.Screen
{
	public class MenuScreen : GameScreen
	{
		String _MenuTitle;
		List<MenuEntry> _MenuEntries = new List<MenuEntry>();
		int _SelectedEntry = 3;

		private InputAction _MenuUpAction;
		private InputAction _MenuDownAction;
		private InputAction _MenuSelectAction;
		private InputAction _MenuCancel;

		public MenuScreen( String title )
		{
			_MenuTitle = title;

			TransitionOnTime = TimeSpan.FromSeconds( 0.5 );
			TransitionOffTime = TimeSpan.FromSeconds( 0.5 );

			_MenuUpAction = new InputAction(
				new Keys[] { Keys.Up },
				new MouseButtons[] { MouseButtons.ScrollUp }, true );
			_MenuDownAction = new InputAction(
				new Keys[] { Keys.Down },
				new MouseButtons[] { MouseButtons.ScrollDown }, true );
			_MenuSelectAction = new InputAction(
				new Keys[] { Keys.Enter },
				new MouseButtons[] { MouseButtons.LeftButton },
				true
				);
			_MenuCancel = new InputAction(
				new Keys[] { Keys.Escape },
				null, 
				true );
		}

		protected IList<MenuEntry> MenuEntries
		{
			get { return _MenuEntries; }
		}

		public override void HandleInput( GameTime gameTime )
		{
			base.HandleInput( gameTime );

			if ( _MenuUpAction.Evaluate() )
				_SelectedEntry--;
			if ( _MenuDownAction.Evaluate() )
				_SelectedEntry++;

			if ( _MenuSelectAction.Evaluate() )
			{
				OnEnterySelected( _SelectedEntry );
			}
			if ( _MenuCancel.Evaluate() )
			{
				OnCancel();
			}

#if WINDOWS
			if ( InputManager.MouseMoved )
			{
				for ( int i = 0; i < _MenuEntries.Count; i++ )
				{
					if ( _MenuEntries[ i ].IsMouseOver( ScreenManager.DefaultFont, InputManager.CurrentMouseState.X, InputManager.CurrentMouseState.Y ) )
						_SelectedEntry = i;
				}
			}
#endif

			if ( _SelectedEntry < 0 )
				_SelectedEntry = _MenuEntries.Count - 1;
			if ( _SelectedEntry > _MenuEntries.Count - 1 )
				_SelectedEntry = 0;
		}

		protected virtual void OnEnterySelected( int index )
		{
			_MenuEntries[ index ].OnSelectEntry();
		}
		protected virtual void OnCancel()
		{
			ExitScren();
		}
		protected void OnCancel( object sender, EventArgs e )
		{
			OnCancel( );
		}

		public override void Update( GameTime gameTime, 
			bool otherScreenHasFocus, bool coveredByOtherScreen )
		{
			base.Update( gameTime, otherScreenHasFocus, coveredByOtherScreen );

			for ( int i = 0; i < _MenuEntries.Count; i++ )
			{
				bool isSelected = ( i == _SelectedEntry );

				_MenuEntries[ i ].Update( this, isSelected, gameTime );
			}
		}

		public override void Draw( GameTime gameTime )
		{
			var graphics = ScreenManager.GraphicsDevice;
			var spriteBatch = ScreenManager.SpriteBatch;
			var font = ScreenManager.DefaultFont;

			UpdateMenuEntryLocations();

			float transitionOffset = ( float )Math.Pow( TransitionPosition, 2 );

			var titlePost = new Vector2( graphics.Viewport.Width / 2, 65 );
			var titleOrigin = font.MeasureString( _MenuTitle ) / 2;
			var titleScale = 1.25f;
			Color titleColor = new Color( 255, 192, 192 ) * TransitionAlpha;
			titlePost.Y -= transitionOffset * 100;

			spriteBatch.Begin();
			spriteBatch.DrawString( font, _MenuTitle, titlePost, titleColor, 0, titleOrigin, titleScale, SpriteEffects.None, 0 );

			for ( int i = 0; i < _MenuEntries.Count; i++ )
			{
				bool isSelected = ( i == _SelectedEntry );
				MenuEntry entry = _MenuEntries[ i ];
				entry.Draw( this, isSelected, gameTime );
			}

			spriteBatch.End();
		}

		protected virtual void UpdateMenuEntryLocations()
		{
			float transitionOffset = ( float )Math.Pow( TransitionPosition, 2 );
			Vector2 position = new Vector2( 0f, 120f );
			var center = ScreenManager.GraphicsDevice.Viewport.Width / 2;

			for ( int i = 0; i < _MenuEntries.Count; i++ )
			{
				MenuEntry entry = _MenuEntries[ i ];
				position.X = center - entry.GetWidth( ScreenManager.DefaultFont ) / 2;

				if ( ScreenState == GameScreenState.TransitionOn )
					position.X -= transitionOffset * 256;
				else
					position.X += transitionOffset * 512;

				entry.Position = position;

				position.Y += entry.GetHeight( ScreenManager.DefaultFont ) + 10;
			}
		}
	}
}
