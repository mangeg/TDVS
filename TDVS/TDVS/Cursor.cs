using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TDVS.Settings;
using Microsoft.Xna.Framework.Content;

namespace TDVS
{
	public static class Cursor
	{
		private static Game _Game;
		private static SpriteBatch _SpriteBatch;
		private static Texture2D _CursorTexture;
		private static bool _IsVisible;
		private static bool _ClipToWindow;

		private static Rectangle _InitialRect;

		static Cursor()
		{
			_IsVisible = true;
			_ClipToWindow = false;
		}

		public static void Initialize( Game game )
		{
			_Game = game;
			Native.GetClipCursor( ref _InitialRect );
			Clip( _ClipToWindow );
		}

		public static void LoadContent( SpriteBatch spriteBatch, ContentManager Content )
		{
			_SpriteBatch = spriteBatch;
			_CursorTexture = Content.Load<Texture2D>( @"Textures\Cursor" );
		}

		public static void UnloadContent()
		{
			Native.ClipCursor( ref _InitialRect );
		}

		public static void Update( GameTime gameTime )
		{
		}

		public static void Draw( GameTime gameTime )
		{
			if ( !_IsVisible ) return;

			Vector2 p = new Vector2( InputManager.MousePosition.X, InputManager.MousePosition.Y );
			Vector2 p2 = new Vector2( p.X + 1, p.Y + 1 );
			Color c = new Color( SettingsManager.Settings.VideoSettings.MouseColor );

			_SpriteBatch.Draw( _CursorTexture, p2, null, Color.Black, 0f, Vector2.Zero,
				SettingsManager.Settings.VideoSettings.MouseScale, SpriteEffects.None, 1 );
			_SpriteBatch.Draw( _CursorTexture, p, null, c,
				0f, Vector2.Zero,
				SettingsManager.Settings.VideoSettings.MouseScale, SpriteEffects.None, 1 );
		}

		public static bool IsVisible
		{
			get { return _IsVisible; }
			set { _IsVisible = value; }
		}
		public static bool ClipToWindow
		{
			get { return _ClipToWindow; }
			set 
			{ 
				_ClipToWindow = value;
				Clip( _ClipToWindow );
			}
		}

		private static void Clip( bool clip )
		{
			if ( clip )
			{
				Rectangle rect = _Game.Window.ClientBounds;
				rect.Width += rect.X;
				rect.Height += rect.Y;
				Native.ClipCursor( ref rect );
			}
			else
			{
				Native.ClipCursor( ref _InitialRect );
			}
		}
	}
}
