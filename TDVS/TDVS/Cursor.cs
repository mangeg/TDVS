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
	public class Cursor : DrawableGameComponent
	{
		private SpriteBatch _SpriteBatch;
		private Texture2D _CursorTexture;

		private static bool _IsVisible;
		private static bool _ClipToWindow;
		private static Rectangle _InitialRect;

		public Cursor( Game game )
			: base( game )
		{

		}

		static Cursor()
		{
			_IsVisible = true;
			_ClipToWindow = false;
		}

		public override void Initialize()
		{
			Native.GetClipCursor( ref _InitialRect );
			Clip( _ClipToWindow );
		}

		protected override void LoadContent()
		{
			_SpriteBatch = new SpriteBatch( GraphicsDevice );
			_CursorTexture = Game.Content.Load<Texture2D>( @"Textures\Cursor" );
		}

		protected override void UnloadContent()
		{
			Native.ClipCursor( ref _InitialRect );
		}

		public override void Update( GameTime gameTime )
		{
		}

		public override void Draw( GameTime gameTime )
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

		public bool ClipToWindow
		{
			get { return _ClipToWindow; }
			set
			{
				_ClipToWindow = value;
				Clip( _ClipToWindow );
			}
		}

		private void Clip( bool clip )
		{
			if ( clip )
			{
				Rectangle rect = Game.Window.ClientBounds;
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
